using Fit.Demo.Domain;
using Fit.Demo.Specification;

namespace Fit.Demo.Infrastructure.InMemory;

public class InMemoryTagRepository : InMemoryBase<Tag>, ITagApi { }
