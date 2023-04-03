using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Costs.Model;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Domain.VehiculeTypes.Models;
using SuperCarga.Application.Settings;

namespace SuperCarga.Persistence.Database.Init
{
    public static class SuperCargaDbSeed
    {
        public static void AddCosts(this SuperCargaContext ctx)
        {
            var alreadyAdded = ctx.Costs.Any();

            if (alreadyAdded) return;

            ctx.Costs.Add(new Cost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Type = CostType.ServiceFee,
                Value = 5,
                FromDate = DateTime.Now.Date,
                ToDate = null
            });

            ctx.Costs.Add(new Cost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Type = CostType.Loading,
                Value = 50,
                FromDate = DateTime.Now.Date,
                ToDate = null
            });

            ctx.Costs.Add(new Cost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Type = CostType.Unloading,
                Value = 50,
                FromDate = DateTime.Now.Date,
                ToDate = null
            });

            ctx.SaveChanges();
        }

        public static void AddVechiculeTypes(this SuperCargaContext ctx)
        {
            var alreadyAdded = ctx.VehiculeTypes.Any();

            if (alreadyAdded) return;

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "Pickup",
                PricePerKm = 1.34M,
                MaxCargoWeight = 1000,
                MaxCargoLenght = 2,
                MaxCargoWidth = 1,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2D",
                PricePerKm = 1.34M,
                MaxCargoWeight = 5000,
                MaxCargoLenght = 4,
                MaxCargoWidth = 2,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2DA",
                PricePerKm = 1.4M,
                MaxCargoWeight = 8000,
                MaxCargoLenght = 5,
                MaxCargoWidth = 3,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2DB",
                PricePerKm = 1.52M,
                MaxCargoWeight = 15000,
                MaxCargoLenght = 6,
                MaxCargoWidth = 4,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "3A",
                PricePerKm = 1.75M,
                MaxCargoWeight = 19000,
                MaxCargoLenght = 8,
                MaxCargoWidth = 5,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2S1",
                PricePerKm = 1.91M,
                MaxCargoWeight = 21000,
                MaxCargoLenght = 13,
                MaxCargoWidth = 6,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2S2",
                PricePerKm = 1.91M,
                MaxCargoWeight = 28000,
                MaxCargoLenght = 13,
                MaxCargoWidth = 6,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "2S3",
                PricePerKm = 1.99M,
                MaxCargoWeight = 32000,
                MaxCargoLenght = 13,
                MaxCargoWidth = 6,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "3S2",
                PricePerKm = 2.03M,
                MaxCargoWeight = 36000,
                MaxCargoLenght = 13,
                MaxCargoWidth = 6,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "3S3",
                PricePerKm = 2.1M,
                MaxCargoWeight = 41000,
                MaxCargoLenght = 13,
                MaxCargoWidth = 6,
                MaxCargoHeight = 2.4M,
                RequireDocuments = false,
            });

            ctx.VehiculeTypes.Add(new VehiculeType
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = "Custom",
                PricePerKm = 0,
                MaxCargoWeight = decimal.MaxValue,
                MaxCargoLenght = decimal.MaxValue,
                MaxCargoWidth = decimal.MaxValue,
                MaxCargoHeight = decimal.MaxValue,
                RequireDocuments = true,
            });

            ctx.SaveChanges();
        }

        public static void AddRoles(this SuperCargaContext ctx)
        {
            var alreadyAdded = ctx.Roles.Any();

            if (alreadyAdded) return;

            ctx.Roles.Add(new Role
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = Roles.Admin
            });

            ctx.Roles.Add(new Role
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = Roles.Customer
            });

            ctx.Roles.Add(new Role
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = Roles.Driver
            });

            ctx.SaveChanges();
        }

        public static void AddUsers(this SuperCargaContext ctx, UserImageStoreConfig usersImageStoreConfig)
        {
            var alreadyAdded = ctx.Users.Any();

            if (alreadyAdded) return;

            #region customer

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            };

            var customerRole = ctx.Roles.Where(x => x.Name == Roles.Customer).Single();
            var customerUser = new User
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                CustomerId = customer.Id,
                Email = "test_customer@sc.com",
                FirstName = "Customer",
                LastName = "Test",
                TermsAccepted = true,
                IsActive = true,
                ImagePath = $"{usersImageStoreConfig.Path}/{usersImageStoreConfig.Default}",
                Roles = new List<Role> { customerRole },
                VerificationState = VerificationState.Verifed
            };
            customerUser.EmailNormalized = customerUser.Email.ToUpper();
            customerUser.Password = new PasswordHasher<User>().HashPassword(customerUser, "testCustomer");

            var customerFinance = new Finance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                UserId = customerUser.Id,
                Balance = 10000000,
                AvailableBalance = 10000000
            };

            var customerPayment = new Payment
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Operation = FinanceOperation.Deposit,
                OperationValue = 10000000,
                FromUserBalanceBefore = null,
                FromUserBalanceAfter = null,
                FromUserId = null,
                ToUserBalanceBefore = 0,
                ToUserBalanceAfter = 10000000,
                ToUserId = customerUser.Id,
                RelatedContractId = null
            };

            ctx.Customers.Add(customer);
            ctx.Users.Add(customerUser);
            ctx.Finances.Add(customerFinance);
            ctx.Payments.Add(customerPayment);

            #endregion

            #region driver

            var vechiculeType = ctx.VehiculeTypes.Where(x => x.Name == "2DB").FirstOrDefault();
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                VehiculeTypeId = vechiculeType.Id
            };

            var driverRole = ctx.Roles.Where(x => x.Name == Roles.Driver).Single();
            var driverUser = new User
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                DriverId = driver.Id,
                Email = "test_driver@sc.com",
                FirstName = "Driver",
                LastName = "Test",
                TermsAccepted = true,
                IsActive = true,
                ImagePath = $"{usersImageStoreConfig.Path}/{usersImageStoreConfig.Default}",
                Roles = new List<Role> { driverRole },
                VerificationState = VerificationState.Verifed
            };
            driverUser.EmailNormalized = driverUser.Email.ToUpper();
            driverUser.Password = new PasswordHasher<User>().HashPassword(driverUser, "testDriver");

            var driverFinance = new Finance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                UserId = driverUser.Id,
                Balance = 0,
                AvailableBalance = 0
            };

            ctx.Drivers.Add(driver);
            ctx.Users.Add(driverUser);
            ctx.Finances.Add(driverFinance);    

            #endregion

            #region admin

            var adminRole = ctx.Roles.Where(x => x.Name == Roles.Admin).Single();
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Email = "admin@sc.com",
                FirstName = "Admin",
                LastName = "Admin",
                TermsAccepted = true,
                IsActive = true,
                ImagePath = $"{usersImageStoreConfig.Path}/{usersImageStoreConfig.Default}",
                Roles = new List<Role> { adminRole },
                VerificationState = VerificationState.Verifed
            };

            adminUser.EmailNormalized = adminUser.Email.ToUpper();
            adminUser.Password = new PasswordHasher<User>().HashPassword(driverUser, "admin");

            var adminFinance = new Finance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                UserId = adminUser.Id,
                Balance = 0,
                AvailableBalance = 0
            };

            ctx.Users.Add(adminUser);
            ctx.Finances.Add(adminFinance);

            #endregion

            ctx.SaveChanges();

        }

        public static void AddJobs(this SuperCargaContext ctx, ICostsService costsService, IDriversService driversService, int howMany)
        {
            var alreadyAdded = ctx.Jobs.Any();

            if (alreadyAdded) return;

            var driver = ctx.Drivers
                .Include(x => x.User)
                .Where(x => x.User.Email == "test_driver@sc.com")
                .Single();

            var customer = ctx.Customers
                .Include(x => x.User)
                .Where(x => x.User.Email == "test_customer@sc.com")
                .Single();

            var admin = ctx.Users
                .Where(x => x.Email == "admin@sc.com")
                .Single();

            var vehicules = ctx.VehiculeTypes.ToList();

            var r = new Random();

            var i = 1;
            var loop = true;

            do
            {
                var added = ctx.GenerateJob(customer, driver, admin, vehicules, costsService, i, r);
                if (added)
                {
                    i++;
                    if (i == howMany)
                    {
                        loop = false;
                    }
                }

            }
            while (loop);

            ctx.SaveChanges();

            driversService.UpdateDriverRates(driver.Id, true).GetAwaiter().GetResult();
        }

        private static bool GenerateJob(
            this SuperCargaContext ctx,
            Customer customer, 
            Driver driver, 
            User admin,
            List<VehiculeType> vehicules, 
            ICostsService costsService,
            int index,
            Random r)
        {
            var added = false;

            var post = GenerateJob(customer, vehicules, costsService, index, r);

            if (post.VehiculeTypeId == driver.VehiculeTypeId && post.State == JobState.Active && RandomBool(6, r))
            {
                added = true;

                var proposal = GenerateProposal(post, driver, r);

                ctx.Jobs.Add(post); 
                ctx.Proposals.Add(proposal);

                //proposal - hired
                //contract - started
                if (RandomBool(3, r))
                {
                    proposal.State = ProposalState.Hired;

                    var contract = GenerateContract(post, customer, driver, costsService, proposal);
                    ctx.Contracts.Add(contract);
                    var history = ChangeContractState(contract, ContractState.Created);
                    ctx.ContractHistories.Add(history);
                    var history1 = ChangeContractState(contract, ContractState.Started);
                    ctx.ContractHistories.Add(history1);

                    var customerFin = ctx.Finances
                        .Where(x => x.UserId == customer.User.Id)
                        .FirstOrDefault();

                    var hold = AddHold(customerFin, contract);
                    ctx.Add(hold);

                    //closed proposal and contract
                    if (RandomBool(2, r))
                    {
                        var driverFin = ctx.Finances
                        .Where(x => x.UserId == driver.User.Id)
                        .FirstOrDefault();

                        var adminFin = ctx.Finances
                            .Where(x => x.UserId == admin.Id)
                            .FirstOrDefault();

                        proposal.State = ProposalState.Closed;
                        contract.Rating = r.Next(1, 6);
                        contract.RatingComment = "Rating comment";

                        var history2 = ChangeContractState(contract, ContractState.Closed);
                        ctx.ContractHistories.Add(history);        

                        //remove hold
                        RemoveHold(customerFin, hold);
                        ctx.Remove(hold);

                        var feePayment = AddFeePayment(contract, customerFin, adminFin, customer, admin);
                        ctx.Payments.Add(feePayment);

                        var driversFirstPayment = AddDriversPayment(contract.TotalPrice / 2 - contract.ServiceFee, customerFin, customer, contract, driverFin, driver);
                        ctx.Payments.Add(driversFirstPayment);

                        var driversSecondPayment = AddDriversPayment(contract.TotalPrice / 2, customerFin, customer, contract, driverFin, driver);
                        ctx.Payments.Add(driversSecondPayment);

                        customer.Spends += contract.TotalPrice;
                        customer.FinalizedContracts++;

                        ctx.SaveChanges();
                    }
                }
                //proposal accepted
                //contract created
                else if (proposal.State == ProposalState.Accepted)
                {
                    var contract = GenerateContract(post, customer, driver, costsService, proposal);
                    ctx.Contracts.Add(contract);
                    var history = ChangeContractState(contract, ContractState.Created);
                    ctx.ContractHistories.Add(history);
                }
            }
            //job added, no proposal and contract
            else if(!RandomBool(6, r))
            {
                ctx.Jobs.Add(post);
                added = true;
            }

            return added;  
        }

        private static Payment AddFeePayment(Contract contract, Finance customerFin, Finance adminFin, Customer customer, User admin)
        {
            var paymentFee = new Payment
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Operation = FinanceOperation.Fee,
                OperationValue = contract.ServiceFee,
                FromUserBalanceBefore = customerFin.Balance,
                FromUserBalanceAfter = customerFin.Balance - contract.ServiceFee,
                FromUserId = customer.User.Id,
                ToUserBalanceBefore = adminFin.Balance,
                ToUserBalanceAfter = adminFin.Balance + contract.ServiceFee,
                ToUserId = admin.Id,
                RelatedContractId = contract.Id
            };
            
            customerFin.Balance = customerFin.Balance - contract.ServiceFee;
            customerFin.AvailableBalance = customerFin.AvailableBalance - contract.ServiceFee;
            adminFin.Balance = adminFin.Balance + contract.ServiceFee;
            adminFin.AvailableBalance = adminFin.AvailableBalance + contract.ServiceFee;

            return paymentFee;
        }

        private static Payment AddDriversPayment(decimal driversPayment, Finance customerFin, Customer customer, Contract contract, Finance driverFin, Driver driver)
        {
            var paymentForDriver = new Payment
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Operation = FinanceOperation.Transfer,
                OperationValue = driversPayment,
                FromUserBalanceBefore = customerFin.Balance,
                FromUserBalanceAfter = customerFin.Balance - driversPayment,
                FromUserId = customer.User.Id,
                ToUserBalanceBefore = driverFin.Balance,
                ToUserBalanceAfter = driverFin.Balance + driversPayment,
                ToUserId = driver.User.Id,
                RelatedContractId = contract.Id
            };
            customerFin.Balance = customerFin.Balance - driversPayment;
            customerFin.AvailableBalance = customerFin.AvailableBalance - driversPayment;
            driverFin.Balance = driverFin.Balance + driversPayment;
            driverFin.AvailableBalance = driverFin.AvailableBalance + driversPayment;

            return paymentForDriver;
        }

        private static BalanceHold AddHold(Finance customerFin, Contract contract)
        {
            var hold = new BalanceHold
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = customerFin.Id,
                Value = contract.TotalPrice / 2,
                RelatedContractId = contract.Id
            };

            customerFin.AvailableBalance = customerFin.AvailableBalance - (contract.TotalPrice / 2);

            return hold;
        }

        private static void RemoveHold(Finance customerFin, BalanceHold hold)
        {
            customerFin.AvailableBalance = customerFin.AvailableBalance + hold.Value;
        }


        private static ContractHistory ChangeContractState(Contract contract, string state)
        {
            contract.State = state;

            return new ContractHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                State = contract.State
            };
        }

        private static Contract GenerateContract(Job post, Customer customer, Driver driver, ICostsService costsService, Proposal proposal)
        {
            var costs = costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
            {
                PricePerKm = proposal.PricePerKm,
                Distance = post.Distance,
                RequireLoadingCrew = post.RequireLoadingCrew,
                RequireUnloadingCrew = post.RequireUnloadingCrew
            }).GetAwaiter().GetResult();

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ProposalId = proposal.Id,
                JobId = post.Id,
                DriverId = driver.Id,
                CustomerId = customer.Id,
                State = ContractState.Created,
                PricePerKm = costs.PricePerKm,
                PricePerDistance = costs.PricePerDistance,
                TotalPrice = costs.TotalPrice,
                ServiceFee = costs.ServiceFee,
                Price = costs.Price,
                PaymentState = ContractPaymentState.OnDeliveryConfirmation
            };

            contract.Additions = costs.Additions.Select(x => new ContractAdditionalCost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList();

            contract.History = new List<ContractHistory>();

            //contract.History = new List<ContractHistory>
            //{ 
            //    new ContractHistory
            //    {
            //        Id = Guid.NewGuid(),
            //        Created = DateTime.Now,
            //        ContractId = contract.Id,
            //        State = contract.State
            //    }
            //};

            return contract;
        }

        private static Proposal GenerateProposal(Job post, Driver driver, Random r)
        {
            var proposal = new Proposal
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                JobId = post.Id,
                DriverId = driver.Id,
                PricePerKm = post.PricePerKm + (r.Next(10, 130) * 0.1M),
                State = RandomBool(2, r) ? ProposalState.Pending : ProposalState.Accepted
            };

            if (proposal.State == ProposalState.Accepted)
                proposal.Checked = true;

            if (proposal.State == ProposalState.Pending)
                proposal.Checked = RandomBool(2, r);

            return proposal;
        }

        private static Job GenerateJob(
            Customer customer,
            List<VehiculeType> vehicules,
            ICostsService costsService,
            int index,
            Random r)
        {
            var post = new Job();
            post.Id = Guid.NewGuid();
            post.Created = DateTime.Now;
            post.CustomerId = customer.Id;
            post.CargoWeight = r.Next(1, 80) * 500;
            post.CargoHeight = r.Next(1, 24) * 0.1M;
            post.CargoLenght = r.Next(1, 130) * 0.1M;
            post.CargoWidth = r.Next(1, 65) * 0.1M;
            post.OriginPostCode = $"{index}{index}-{index}{index}{index}";
            post.OriginStreet = $"Street {index}";
            post.OriginCity = $"City {index}";
            post.DestinationPostCode = $"{index}{index}{index}-{index}{index}";
            post.DestinationStreet = $"Street {index}{index}";
            post.DestinationCity = $"City {index}{index}";
            post.RequireLoadingCrew = RandomBool(2, r);
            post.RequireUnloadingCrew = RandomBool(2, r);
            post.Description = $"Job description {index}";
            post.State = RandomBool(4, r) ? JobState.Active : JobState.Closed;
            post.Tittle = $"Job {index}";
            post.PickupDate = DateTime.Now.AddDays(r.Next(2, 10));
            post.DeliveryDate = post.PickupDate.AddDays(r.Next(10, 100));
            post.Distance = r.Next(100, 10000);

            var vehiculeType = vehicules
                .Where(x => x.MaxCargoWeight > post.CargoWeight)
                .Where(x => x.MaxCargoLenght > post.CargoLenght)
                .Where(x => x.MaxCargoWidth > post.CargoWidth)
                .Where(x => x.MaxCargoHeight > post.CargoHeight)
                .OrderBy(x => x.MaxCargoWeight)
                .FirstOrDefault();

            post.VehiculeTypeId = vehiculeType.Id;

            var jobcosts = costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
            {
                PricePerKm = vehiculeType.PricePerKm,
                Distance = post.Distance,
                RequireLoadingCrew = post.RequireLoadingCrew,
                RequireUnloadingCrew = post.RequireUnloadingCrew
            }).GetAwaiter().GetResult();

            post.PricePerKm = jobcosts.PricePerKm;
            post.PricePerDistance = jobcosts.PricePerDistance;
            post.TotalPrice = jobcosts.TotalPrice;
            post.ServiceFee = jobcosts.ServiceFee;
            post.Price = jobcosts.Price;

            post.Additions = jobcosts.Additions.Select(x => new JobAdditionalCost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                JobId = post.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList();

            return post;
        }

        private static bool RandomBool(int max, Random r) => r.Next(0, max) == 0 ? false : true;

    }
}
