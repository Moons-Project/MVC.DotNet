using System;
using System.Collections.Generic;
using System.Reflection;
using MVC.Core.Extention;
using MVC.Engine;

namespace MVC.Core {
  /// <summary>
  /// 核心逻辑部分
  /// </summary>
  internal static class Core {

    private static readonly Dictionary<Type, Object> modelDict;
    private static readonly Dictionary<Type, Object> viewDict;
    private static readonly Dictionary<Type, Object> controllerDict;
    private static readonly Dictionary<Type, Delegate> commandDict;

    private static readonly GameObject core;
    private static readonly GameObject controllers;

    public static Assembly MainAssembly {
      get;
      private set;
    }

    static Core() {
      MainAssembly = Assembly.Load("MVC.DotNet");

      modelDict = new Dictionary<Type, object>();
      viewDict = new Dictionary<Type, object>();
      controllerDict = new Dictionary<Type, object>();
      commandDict = new Dictionary<Type, Delegate>();

      core = new GameObject("Core", new BaseBehavior());
      controllers = new GameObject("Controller", new BaseBehavior());
    }

    public static TModel GetModel<TModel>() where TModel : Model, new() {

      Object model = null;
      if (!modelDict.TryGetValue(typeof(TModel), out model)) {
        model = new TModel();
        modelDict.Add(typeof(TModel), model);
      }
      return model as TModel;
    }

    public static TView GetView<TView>() where TView : View, new() {
      Object view = null;
      if (!viewDict.TryGetValue(typeof(TView), out view) ||
        view as TView == null) {
        // create new game object
        GameObject viewObj = new GameObject(typeof(TView).Name, new TView());
        view = viewObj.behavior as TView;
        viewDict.Add(typeof(TView), view);
      }
      return view as TView;
    }

    private static Object GetController(ICommand cmd) {
      try {
        return GetController(cmd.GetController());
      } catch (CoreException) {
        throw new CoreException($"[Core.GetController] The controller class depend on {cmd.GetType().Name} named {cmd.GetController().Name} doesn't inherit from Controller<,>");
      }
    }

    public static Object GetController(Type controllerType) {
      Object controller = null;

      if (!controllerDict.TryGetValue(controllerType, out controller)) {

        // check inherit
        // if (!controllerType.IsSubclassOfRawGeneric(typeof(Controller<,>)) &&
        //   !controllerType.IsSubclassOfRawGeneric(typeof(Controller<>))) {
        if (!TypeExtension.IsSubClassOfRawGeneric(
            controllerType, typeof(Controller<,>)) &&
          !TypeExtension.IsSubClassOfRawGeneric(
            controllerType, typeof(Controller<>))) {
          throw new CoreException($"[Core.GetController] The controller class named {controllerType.Name} doesn't inherit from Controller");
        }

        GameObject controllerObj = new GameObject(controllerType.Name, controllerType);
        controller = controllerObj.behavior;
        controllerDict.Add(controllerType, controller);
      }

      return controller;
    }

    public static void Call<TCommand>(TCommand cmd) where TCommand : ICommand {

      Type cmdType = typeof(TCommand);
      Delegate action = null;

      if (!commandDict.TryGetValue(cmdType, out action)) {
        Type controllerType = cmd.GetController();
        Object controller = GetController(cmd);


        MethodInfo info = controllerType.GetMethod($"On{cmd.GetType().Name}",
          BindingFlags.Public | BindingFlags.Instance);
        if (info == null) {
          throw new CoreException($"[Core.Call] Unhandled Command: {cmd.GetType().Name} for {controllerType.Name}");
        }

        action = info.CreateDelegate(typeof(Action<TCommand>), controller);
        commandDict.Add(cmdType, action);

      }
        (action as Action<TCommand>) (cmd);
    }

    public static TResult Post<TCommand, TResult>(TCommand cmd)
    where TCommand : ICommand {
      Type cmdType = typeof(TCommand);
      Delegate func = null;

      if (!commandDict.TryGetValue(cmdType, out func)) {
        Type controllerType = cmd.GetController();
        Object controller = GetController(cmd);

        MethodInfo info = controllerType.GetMethod($"On{cmd.GetType().Name}",
          BindingFlags.Public | BindingFlags.Instance);
        if (info == null) {
          throw new CoreException($"[Core.Call] Unhandled Command: {cmd.GetType().Name} for {controllerType.Name}");
        }

        func = info.CreateDelegate(typeof(Func<TCommand, TResult>), controller);
        commandDict.Add(cmdType, func);

      }
      return (func as Func<TCommand, TResult>) (cmd);
    }

    public static void Call(string cmdFullName) {
      Type commandType = MainAssembly.GetType(cmdFullName);
      Command cmd = Activator.CreateInstance(commandType) as Command;
      Delegate action = null;

      if (!commandDict.TryGetValue(commandType, out action)) {
        Type controllerType = cmd.GetController();
        object controller = GetController(cmd);
        MethodInfo info = controllerType.GetMethod(string.Format("On{0}", cmd.GetType().Name), BindingFlags.Public | BindingFlags.Instance);
        if (info == null) {
          throw new CoreException($"[Core.Call] Unhandled Command : {cmd.GetType().Name} for {controllerType.Name}");
        }
        info.Invoke(controller, new object[] { cmd });
      } else action.DynamicInvoke(cmd);
    }

    public static object Post(ICommand cmd) {
      Type commandType = cmd.GetType();
      Delegate func = null;
      if (!commandDict.TryGetValue(commandType, out func)) {
        Type controllerType = cmd.GetController();
        object controller = GetController(cmd);
        MethodInfo methodInfo = controllerType.GetMethod(($"On{cmd.GetType().Name}"), BindingFlags.Public | BindingFlags.Instance);
        if (methodInfo == null) {
          throw new CoreException($"[Core.Post]Unhandled Command : {cmd.GetType().Name} for {controllerType.Name}");
        }
        return methodInfo.Invoke(controller, new object[] { cmd });
      } else return func.DynamicInvoke(cmd);
    }
  }
}