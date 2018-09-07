using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;

namespace HopeLine.Service.CoreServices
{
    //TODO : implement interface
    public class CommonResourceService //: ICommonResource
    {
        private readonly IRepository<Resource> _resourceRepo;
        private readonly IRepository<Community> _communityRepo;

        public CommonResourceService(IRepository<Resource> resourceRepo,
                                    IRepository<Community> communityRepo)
        {
            _resourceRepo = resourceRepo;
            _communityRepo = communityRepo;

        }
    }
}
