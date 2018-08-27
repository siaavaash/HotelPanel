using Service.ServiceModel.GIATAModels;

namespace Service.ServiceModel.PublicModels
{
    public class Error
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public bool IsApiError { get; set; }
    }
    public class ResultDataModel
    {
        public bool Success { get; set; }
        public Error Error { get; set; }
        public IResponse Model { get; set; }
    }
}
