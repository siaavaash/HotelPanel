namespace Service.ServiceModel.PublicModels
{
    public class Error
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public bool IsApiError { get; set; }
    }
    public class ResultDataModel<TModel> where TModel : class
    {
        public bool Success { get; set; }
        public Error Error { get; set; }
        public TModel Model { get; set; }
    }
}
