using AutoMapper;

namespace PointsApp.DomainModel.Mappings;

public interface IMapWith
{
    void Mapping(Profile profile);
}

public interface IMapWith<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}