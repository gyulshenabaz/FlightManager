namespace FlightManager.Common.AutoMapping.Interfaces
{
    using AutoMapper;
    
    public interface ICustomMapping
    {
        void CreateMappings(IProfileExpression configuration);
    }
}