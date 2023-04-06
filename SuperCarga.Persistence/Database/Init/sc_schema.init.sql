Create schema if not exists sc;
GRANT ALL PRIVILEGES ON SCHEMA sc TO sc;

drop table if exists sc.payments;
drop table if exists sc.balance_holds;
drop table if exists sc.finances;
drop table if exists sc.contract_additional_costs;
drop table if exists sc.contract_histories;
drop table if exists sc.contracts;
drop table if exists sc.customer_favorite_proposals;
drop table if exists sc.proposals;
drop table if exists sc.driver_favorite_jobs;
drop table if exists sc.job_additional_costs;
drop table if exists sc.jobs;
drop table if exists sc.free_estimation_history;
drop table if exists sc.users_roles;
drop table if exists sc.users;
drop table if exists sc.roles;
drop table if exists sc.drivers;
drop table if exists sc.customers;
drop table if exists sc.vehicule_types;
drop table if exists sc.costs;
drop table if exists sc.chats;
drop table if exists sc.chat_attachments;

create table sc.costs (
	id uuid,
	created timestamp without time zone not null default now(),
	type varchar not null,
	value numeric not null,
	from_date timestamp without time zone not null,
	to_date timestamp without time zone,
	constraint PK_costs primary key (id)
);
GRANT ALL PRIVILEGES ON TABLE sc.costs TO sc;

create table sc.vehicule_types (
	id uuid,
	created timestamp without time zone not null default now(),
	name varchar not null,
	price_per_km numeric not null,
	max_cargo_weight numeric not null,
	max_cargo_lenght numeric not null,
	max_cargo_width numeric not null,
	max_cargo_height numeric not null,
	require_documents boolean not null,
	constraint PK_vehicule_types primary key (id)
);
GRANT ALL PRIVILEGES ON TABLE sc.vehicule_types TO sc;

create table sc.customers (
	id uuid,
	created timestamp without time zone not null default now(),
	id_document_path varchar,
	constraint PK_customers primary key (id)
);
GRANT ALL PRIVILEGES ON TABLE sc.customers TO sc;

create table sc.drivers (
	id uuid,
	created timestamp without time zone not null default now(),
	vehicule_type_id uuid not null,
	driving_license_path varchar,
	contracts integer not null,
	rated_contracts integer not null,
	rating numeric,
	constraint PK_drivers primary key (id),
	constraint FK_drivers_vehicule_types foreign key (vehicule_type_id) references sc.vehicule_types (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.drivers TO sc;

create table sc.roles (
	id uuid,
	created timestamp without time zone not null,
	name varchar not null,
	constraint PK_roles primary key (id)
);
GRANT ALL PRIVILEGES ON TABLE sc.roles TO sc;

create table sc.users (
	id uuid,
	created timestamp without time zone not null,
	email varchar not null,
	email_normalized varchar not null,
	first_name varchar not null,
	last_name varchar not null,
	customer_id uuid default null,
	driver_id uuid default null,
	password varchar not null,
	is_active boolean not null, 
	terms_accepted boolean not null, 
	refresh_token varchar default null,
	refresh_token_expiry timestamp without time zone default null,
	image_path varchar not null,
	verification_state varchar not null,
	constraint PK_users primary key (id),
	constraint FK_users_customers foreign key (customer_id) references sc.customers (id) match simple,
	constraint FK_users_drivers foreign key (driver_id) references sc.drivers (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.users TO sc;

create table sc.users_roles (
	user_id uuid not null,
	role_id uuid not null,
	CONSTRAINT PK_users_roles PRIMARY KEY (user_id, role_id),
    CONSTRAINT FK_users_roles_users FOREIGN KEY (user_id) REFERENCES sc.users (id) match simple,
    CONSTRAINT FK_users_roles_roles FOREIGN KEY (role_id) REFERENCES sc.roles (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.users_roles TO sc;

create table sc.free_estimation_history (
	id uuid,
	created timestamp without time zone not null default now(),
	cargo_weight numeric not null,
	cargo_width numeric not null,
	cargo_height numeric not null,
	cargo_lenght numeric not null,
	require_loading_crew boolean not null,
	require_unloading_crew boolean not null,
	email varchar not null,
	customer_name varchar not null,
	estimated_distance integer not null,
	result_vehicule_type_id uuid not null,
	result_price_per_km numeric not null,
	result_estimated_cost numeric not null,
	constraint PK_free_estimation_history primary key (id),
	constraint FK_free_estimation_history_vehicule_types foreign key (result_vehicule_type_id) references sc.vehicule_types (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.free_estimation_history TO sc;

create table sc.jobs (
	id uuid,
	created timestamp without time zone not null default now(),
	customer_id uuid not null,
	cargo_weight numeric not null,
	cargo_width numeric not null,
	cargo_height numeric not null,
	cargo_lenght numeric not null,
	origin_city varchar not null,
	origin_street varchar not null,
	origin_post_code varchar not null,
	destination_city varchar not null,
	destination_street varchar not null,
	destination_post_code varchar not null,
	require_loading_crew boolean not null,
	require_unloading_crew boolean not null,
	description varchar not null,
	tittle varchar not null,
	distance integer not null,
	state varchar not null,
	vehicule_type_id uuid not null,
	price_per_km numeric not null,
	price_per_distance numeric not null,
	price numeric not null,
	service_fee numeric not null,
	total_price numeric not null,
	pickup_date timestamp without time zone not null,
	delivery_date timestamp without time zone not null,
	cargo_image_path varchar,
	constraint PK_jobs primary key (id),
	constraint FK_jobs_customers foreign key (customer_id) references sc.customers (id) match simple,
	constraint FK_jobs_vehicule_types foreign key (vehicule_type_id) references sc.vehicule_types (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.jobs TO sc;

create table sc.job_additional_costs (
	id uuid,
	created timestamp without time zone not null default now(),
	job_id uuid not null,
	name varchar not null,
	price numeric not null,
	constraint PK_job_additional_costs primary key (id),
	constraint FK_job_additional_costs_jobs foreign key (job_id) references sc.jobs (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.job_additional_costs TO sc;

create table sc.driver_favorite_jobs (
	driver_id uuid not null,
	job_id uuid not null,
	CONSTRAINT PK_driver_favorite_jobs PRIMARY KEY (driver_id, job_id),
    CONSTRAINT FK_driver_favorite_jobs_drivers FOREIGN KEY (driver_id) REFERENCES sc.drivers (id) match simple,
    CONSTRAINT FK_driver_favorite_jobs_jobs FOREIGN KEY (job_id) REFERENCES sc.jobs (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.driver_favorite_jobs TO sc;

create table sc.proposals (
	id uuid,
	created timestamp without time zone not null default now(),
	job_id uuid not null,
	driver_id uuid not null,
	price_per_km numeric not null,
	checked boolean not null,
	state varchar not null,
	constraint PK_proposals primary key (id),
	constraint FK_proposals_jobs foreign key (job_id) references sc.jobs (id) match simple,
	constraint FK_proposal_drivers foreign key (driver_id) references sc.drivers (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.proposals TO sc;

create table sc.customer_favorite_proposals (
	customer_id uuid not null,
	proposal_id uuid not null,
	CONSTRAINT PK_customer_favorite_proposals PRIMARY KEY (customer_id, proposal_id),
    CONSTRAINT FK_customer_favorite_proposals_customers FOREIGN KEY (customer_id) REFERENCES sc.customers (id) match simple,
    CONSTRAINT FK_customer_favorite_proposals_proposals FOREIGN KEY (proposal_id) REFERENCES sc.proposals (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.customer_favorite_proposals TO sc;

create table sc.contracts (
	id uuid,
	created timestamp without time zone not null default now(),
	proposal_id uuid not null,
	job_id uuid not null,
	driver_id uuid not null,
	customer_id uuid not null,
	state varchar not null,
	payment_state varchar not null,
	price_per_km numeric not null,
	price_per_distance numeric not null,
	price numeric not null,
	service_fee numeric not null,
	total_price numeric not null,
	pick_up_cargo_image_path varchar,
	pick_up_proof_image_path varchar,
	delivery_cargo_image_path varchar,
	delivery_proof_image_path varchar,
	rating numeric,
	rating_comment varchar, 
	constraint PK_contracts primary key (id),
	constraint FK_contracts_proposals foreign key (proposal_id) references sc.proposals (id) match simple,
	constraint FK_contracts_jobs foreign key (job_id) references sc.jobs (id) match simple,
	constraint FK_contracts_drivers foreign key (driver_id) references sc.drivers (id) match simple,
	constraint FK_contracts_customers foreign key (customer_id) references sc.customers (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.contracts TO sc;

create table sc.contract_histories (
	id uuid,
	created timestamp without time zone not null default now(),
	contract_id uuid not null,
	state varchar not null,
	constraint PK_contract_histories primary key (id),
	constraint FK_contract_histories_contracts foreign key (contract_id) references sc.contracts (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.contract_histories TO sc;

create table sc.contract_additional_costs (
	id uuid,
	created timestamp without time zone not null default now(),
	contract_id uuid not null,
	name varchar not null,
	price numeric not null,
	constraint PK_contract_additional_costs primary key (id),
	constraint FK_contract_additional_costs_contracts foreign key (contract_id) references sc.contracts (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.contract_additional_costs TO sc;

create table sc.finances (
	id uuid,
	created timestamp without time zone not null,
	user_id uuid not null,
	balance numeric not null,
	available_balance numeric not null,
	CONSTRAINT PK_finances PRIMARY KEY (id),
    CONSTRAINT FK_finances_users FOREIGN KEY (user_id) REFERENCES sc.users (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.finances TO sc;

create table sc.balance_holds (
	id uuid,
	created timestamp without time zone not null,
	finance_id uuid not null,
	value numeric not null,
	related_contract_id uuid not null,
	CONSTRAINT PK_balance_holds PRIMARY KEY (id),
    CONSTRAINT FK_balance_holds_finances FOREIGN KEY (finance_id) REFERENCES sc.finances (id) match simple,
    CONSTRAINT FK_balance_holds_related_contract FOREIGN KEY (related_contract_id) REFERENCES sc.contracts (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.balance_holds TO sc;

create table sc.payments (
	id uuid,
	created timestamp without time zone not null,
	operation varchar not null,
	operation_value numeric not null,
	from_user_balance_before numeric,
	from_user_balance_after numeric,	
	from_user_id uuid,
	to_user_balance_before numeric,
	to_user_balance_after numeric,
	to_user_id uuid,
	related_contract_id uuid,
	CONSTRAINT PK_payments PRIMARY KEY (id),
    CONSTRAINT FK_payments_from_user FOREIGN KEY (from_user_id) REFERENCES sc.users (id) match simple,
    CONSTRAINT FK_payments_to_user FOREIGN KEY (to_user_id) REFERENCES sc.users (id) match simple,
    CONSTRAINT FK_payments_related_contract FOREIGN KEY (related_contract_id) REFERENCES sc.contracts (id) match simple
);
GRANT ALL PRIVILEGES ON TABLE sc.payments TO sc;

CREATE TABLE sc.chats (
	id uuid NOT NULL,
	from_user_id uuid NOT NULL,
	to_user_id uuid NOT NULL,
	chat_message text NOT NULL,
	has_attachment bool NOT NULL,
	message_read_datetime timestamp NULL,
	created timestamp NOT NULL DEFAULT now(),
	deleted_datetime timestamp NULL,
	updated_datetime timestamp NOT NULL DEFAULT now(),
	CONSTRAINT PK_chats PRIMARY KEY (id),
	CONSTRAINT FK_to_user_id FOREIGN KEY(from_user_id) REFERENCES sc.users(id)
);
GRANT ALL PRIVILEGES ON TABLE sc.chats TO sc;


CREATE TABLE sc.chat_attachments (
	id uuid NOT NULL,
	chat_id uuid NOT NULL,
	file_name text NOT NULL,
	file_data bytea NOT NULL,
	CONSTRAINT PK_chat_attachments PRIMARY KEY (id), 
	CONSTRAINT FK_chat_id FOREIGN KEY (chat_id) REFERENCES sc.chats(id) ON DELETE CASCADE
);
GRANT ALL PRIVILEGES ON TABLE sc.chat_attachments TO sc;
