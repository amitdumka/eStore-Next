namespace eStoreMobileX.Data.DataModels.Base
{
    public class QueryParam
    {
        public int Id { get; set; }
        /// <summary>
        /// Use this when ID is of string type
        /// </summary>
        public string Ids { get; set; }
        public List<string> Command { get; set; }
        public List<string> Query { get; set; }
        public int Order { get; set; }
        public List<string> Filters { get; set; }
        public int StoreId { get; set; }

    }

}
