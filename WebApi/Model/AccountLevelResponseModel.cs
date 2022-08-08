using Application.Wrappers;

namespace WebApi.Model
{
    public class AccountLevelResponseModel :ResponseBase
    {
        public int Level { get; set; }
        public string LevelName { get; set; }
        private int x;

        public int RightToPost {

            get { return x; }   
            set { x = value; }
        } 

        public AccountLevelResponseModel(int sumOfPost)
        {
            x = sumOfPost;
        }
    }
}
