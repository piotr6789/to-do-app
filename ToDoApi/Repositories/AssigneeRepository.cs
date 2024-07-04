using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Models;
using ToDoApi.Dto;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.Repositories
{
    public class AssigneeRepository : IAssigneeRepository
    {
        private readonly ToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public AssigneeRepository(ToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssigneeDto>> GetAssignees()
        {
            var assignees = await _dbContext.Assignees.ToListAsync();

            return _mapper.Map<IEnumerable<AssigneeDto>>(assignees);
        }
    }
}
