BT-Framework
============

BT Framework is a simple behavior tree framework that can be used to create game AI. It's written for Unity3d.

### Features
1. Minimum dependencies on Unity3d, so it won't pollute your inspector.
2. Extendable to handle customized logic nodes, though classic logic nodes, like sequence, priority selector, parallel, are provided.
3. Communication between local nodes, cross-tree nodes, and other scripts are possible by the use of blackboard system.
4. Enter, Execute, Exit api support for action nodes.


### How to use

1. Create various actions and pre-conditions inheriting from BTAction and BTPrecondition.

2. Create a class inheriting from BTTree, and construct the behavior tree in Init function.

3. Drag the class to a GameObject.

It's this simple!

A simple example:

```csharp
// A class inheriting from BTTree
    
protected override void Init () {
   // Initialize base class
   base.Init();
   
   // Create root node
   _root = new BTPrioritySelector();
   
   // ---Construct the behavior tree---

   // Escape when boss is close
   DistanceClosePrecondition bossClose = new DistanceClosePrecondition("Boss");
   _root.AddChild (new DoRun(bossClose));
   
   // Fight when a goblin is close
   DistanceClosePrecondition goblinClose = new DistanceClosePrecondition("Goblin");
   _root.AddChild (new DoFight(goblinClose));
   
   // Do nothing when boss & goblin not around
   _root.AddChild (new Idle());

}
```
where DistanceClosePrecondition are user defined precondition that inherits from BTPrecondition, and

DoRun, DoFight, Idle are user defined behaviors that inherit from BTAction.



### Example demos:
Ludum Dare 48-hour game jam entry: [Swordsman] (http://ludumdare.com/compo/ludum-dare-30/?action=preview&uid=24851)

Code Demo: [BT Test Improved] (https://github.com/f15gdsy/BT-Test/tree/improved)

Code Demo: [BT Test Original] (https://github.com/f15gdsy/BT-Test/tree/master)

Platformer Demo: [Archer] (https://dl.dropboxusercontent.com/u/27907965/games/Archer/Build_0/Build_0.html)
