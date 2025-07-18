using AutoMapper;
using CVScreeningAPI.DTOs;
using CVScreeningAPI.Models;

namespace CVScreeningAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CreateCandidateDto, Candidate>();
            CreateMap<UpdateCandidateDto, Candidate>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Interview, InterviewDto>();
        }
    }
}
