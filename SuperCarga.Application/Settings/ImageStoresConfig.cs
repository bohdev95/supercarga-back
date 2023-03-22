using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Settings
{
    public class ImageStoresConfig
    {
        public UserImageStoreConfig Users { get; set; }

        public ImageStoreConfig Contracts { get; set; }

        public ImageStoreConfig Jobs { get; set; }
    }

    public class UserImageStoreConfig : ImageStoreConfig
    {
        public string Default { get; set; }
    }

    public class ImageStoreConfig
    {
        public string Path { get; set; }

        public string PhisicalPath { get; set; }

    }


}
