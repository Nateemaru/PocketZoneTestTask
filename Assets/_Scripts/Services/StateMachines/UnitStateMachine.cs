using System;
using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Services.StateMachines
{
   public class UnitStateMachine
   {
      private class Transition
      {
         public Func<bool> Condition { get; }
         public IState To { get; }
         public IState From { get; }

         public Transition(IState from, IState to, Func<bool> condition)
         {
            From = from;
            To = to;
            Condition = condition;
         }
      }

      private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);
      private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
      private readonly List<Transition> _anyTransitions = new List<Transition>();
      private List<Transition> _currentTransitions = new List<Transition>();

      public IState CurrentState { get; private set; }

      public void UpdateMachine()
      {
         Transition transition = GetTransition();
         
         if (transition != null)
         {
            SetState(transition.To);
         }

         CurrentState?.Update();
      }

      public void SetState(IState newState)
      {
         if (newState == CurrentState)
         {
            return;
         }

         CurrentState?.Exit();
         CurrentState = newState;
         _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
         
         if (_currentTransitions == null)
         {
            _currentTransitions = EmptyTransitions;
         }

         CurrentState.Enter();
      }

      public void AddTransition(IState from, IState to, Func<bool> predicate)
      {
         if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
         {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
         }

         transitions.Add(new Transition(from, to, predicate));
      }

      public void AddAnyTransition(IState state, Func<bool> predicate)
      {
         _anyTransitions.Add(new Transition(null, state, predicate));
      }

      private Transition GetTransition()
      {
         foreach (Transition transition in _anyTransitions.Where(transition => transition.Condition()))
         {
            return transition;
         }

         return _currentTransitions.FirstOrDefault(transition => transition.Condition() && transition.From == CurrentState);
      }
   }
}
