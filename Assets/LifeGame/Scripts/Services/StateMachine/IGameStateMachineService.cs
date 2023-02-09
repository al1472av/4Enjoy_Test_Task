
using LifeGame.Services.StateMachine.States;

namespace LifeGame.Services.StateMachine
{
  public interface IGameStateMachineService
  {
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
  }
}