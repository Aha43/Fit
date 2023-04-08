using Fit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Abstraction;

public interface IActorNode
{
    public string ActorName { get; }
    public IActorNode Do<T>() where T : class;
    public IActorNode Do(string name);
    public IActorNode ContinueWith(string name);
    public IActorNode With<T>(string name, T value);
    public void AsCase(string name);
    public void AsSegment(string name);
}
