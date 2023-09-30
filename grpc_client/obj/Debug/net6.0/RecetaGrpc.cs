// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: receta.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

public static partial class Recetas
{
  static readonly string __ServiceName = "Recetas";

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (message is global::Google.Protobuf.IBufferMessage)
    {
      context.SetPayloadLength(message.CalculateSize());
      global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
      context.Complete();
      return;
    }
    #endif
    context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static class __Helper_MessageCache<T>
  {
    public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (__Helper_MessageCache<T>.IsBufferMessage)
    {
      return parser.ParseFrom(context.PayloadAsReadOnlySequence());
    }
    #endif
    return parser.ParseFrom(context.PayloadAsNewBuffer());
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Receta> __Marshaller_Receta = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Receta.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Responsea> __Marshaller_Responsea = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Responsea.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::RecetaEditar> __Marshaller_RecetaEditar = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::RecetaEditar.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::NuloReceta> __Marshaller_NuloReceta = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::NuloReceta.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::RecetaId> __Marshaller_RecetaId = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::RecetaId.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Usuariolog> __Marshaller_Usuariolog = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Usuariolog.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::tiempo> __Marshaller_tiempo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::tiempo.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Puntua> __Marshaller_Puntua = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Puntua.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Prommmm> __Marshaller_Prommmm = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Prommmm.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::doble> __Marshaller_doble = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::doble.Parser));

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Receta, global::Responsea> __Method_AltaReceta = new grpc::Method<global::Receta, global::Responsea>(
      grpc::MethodType.Unary,
      __ServiceName,
      "AltaReceta",
      __Marshaller_Receta,
      __Marshaller_Responsea);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::RecetaEditar, global::Responsea> __Method_EditarReceta = new grpc::Method<global::RecetaEditar, global::Responsea>(
      grpc::MethodType.Unary,
      __ServiceName,
      "EditarReceta",
      __Marshaller_RecetaEditar,
      __Marshaller_Responsea);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::NuloReceta, global::Receta> __Method_TraerRecetas = new grpc::Method<global::NuloReceta, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetas",
      __Marshaller_NuloReceta,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::RecetaId, global::Receta> __Method_TraerRecetaPorId = new grpc::Method<global::RecetaId, global::Receta>(
      grpc::MethodType.Unary,
      __ServiceName,
      "TraerRecetaPorId",
      __Marshaller_RecetaId,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Usuariolog, global::Receta> __Method_TraerRecetasPorUsuario = new grpc::Method<global::Usuariolog, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPorUsuario",
      __Marshaller_Usuariolog,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Usuariolog, global::Receta> __Method_TraerRecetasPorCategoria = new grpc::Method<global::Usuariolog, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPorCategoria",
      __Marshaller_Usuariolog,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Usuariolog, global::Receta> __Method_TraerRecetasPorTitulo = new grpc::Method<global::Usuariolog, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPorTitulo",
      __Marshaller_Usuariolog,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::tiempo, global::Receta> __Method_TraerRecetasPorTiempo = new grpc::Method<global::tiempo, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPorTiempo",
      __Marshaller_tiempo,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Usuariolog, global::Receta> __Method_TraerRecetasPorIngredientes = new grpc::Method<global::Usuariolog, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPorIngredientes",
      __Marshaller_Usuariolog,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::NuloReceta, global::Receta> __Method_TraerRecetasPopulares = new grpc::Method<global::NuloReceta, global::Receta>(
      grpc::MethodType.ServerStreaming,
      __ServiceName,
      "TraerRecetasPopulares",
      __Marshaller_NuloReceta,
      __Marshaller_Receta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Puntua, global::NuloReceta> __Method_AgregarPuntuacion = new grpc::Method<global::Puntua, global::NuloReceta>(
      grpc::MethodType.Unary,
      __ServiceName,
      "AgregarPuntuacion",
      __Marshaller_Puntua,
      __Marshaller_NuloReceta);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::RecetaId, global::Prommmm> __Method_TraerPromedioPuntuacion = new grpc::Method<global::RecetaId, global::Prommmm>(
      grpc::MethodType.Unary,
      __ServiceName,
      "TraerPromedioPuntuacion",
      __Marshaller_RecetaId,
      __Marshaller_Prommmm);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::doble, global::Receta> __Method_TraerRecetasPorTituloyUsuario = new grpc::Method<global::doble, global::Receta>(
      grpc::MethodType.Unary,
      __ServiceName,
      "TraerRecetasPorTituloyUsuario",
      __Marshaller_doble,
      __Marshaller_Receta);

  /// <summary>Service descriptor</summary>
  public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
  {
    get { return global::RecetaReflection.Descriptor.Services[0]; }
  }

  /// <summary>Base class for server-side implementations of Recetas</summary>
  [grpc::BindServiceMethod(typeof(Recetas), "BindService")]
  public abstract partial class RecetasBase
  {
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Responsea> AltaReceta(global::Receta request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Responsea> EditarReceta(global::RecetaEditar request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetas(global::NuloReceta request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Receta> TraerRecetaPorId(global::RecetaId request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPorUsuario(global::Usuariolog request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPorCategoria(global::Usuariolog request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPorTitulo(global::Usuariolog request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPorTiempo(global::tiempo request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPorIngredientes(global::Usuariolog request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task TraerRecetasPopulares(global::NuloReceta request, grpc::IServerStreamWriter<global::Receta> responseStream, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::NuloReceta> AgregarPuntuacion(global::Puntua request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Prommmm> TraerPromedioPuntuacion(global::RecetaId request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Receta> TraerRecetasPorTituloyUsuario(global::doble request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

  }

  /// <summary>Client for Recetas</summary>
  public partial class RecetasClient : grpc::ClientBase<RecetasClient>
  {
    /// <summary>Creates a new client for Recetas</summary>
    /// <param name="channel">The channel to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public RecetasClient(grpc::ChannelBase channel) : base(channel)
    {
    }
    /// <summary>Creates a new client for Recetas that uses a custom <c>CallInvoker</c>.</summary>
    /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public RecetasClient(grpc::CallInvoker callInvoker) : base(callInvoker)
    {
    }
    /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected RecetasClient() : base()
    {
    }
    /// <summary>Protected constructor to allow creation of configured clients.</summary>
    /// <param name="configuration">The client configuration.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected RecetasClient(ClientBaseConfiguration configuration) : base(configuration)
    {
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Responsea AltaReceta(global::Receta request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return AltaReceta(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Responsea AltaReceta(global::Receta request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_AltaReceta, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Responsea> AltaRecetaAsync(global::Receta request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return AltaRecetaAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Responsea> AltaRecetaAsync(global::Receta request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_AltaReceta, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Responsea EditarReceta(global::RecetaEditar request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return EditarReceta(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Responsea EditarReceta(global::RecetaEditar request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_EditarReceta, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Responsea> EditarRecetaAsync(global::RecetaEditar request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return EditarRecetaAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Responsea> EditarRecetaAsync(global::RecetaEditar request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_EditarReceta, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetas(global::NuloReceta request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetas(global::NuloReceta request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetas, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Receta TraerRecetaPorId(global::RecetaId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetaPorId(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Receta TraerRecetaPorId(global::RecetaId request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_TraerRecetaPorId, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Receta> TraerRecetaPorIdAsync(global::RecetaId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetaPorIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Receta> TraerRecetaPorIdAsync(global::RecetaId request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_TraerRecetaPorId, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorUsuario(global::Usuariolog request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorUsuario(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorUsuario(global::Usuariolog request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPorUsuario, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorCategoria(global::Usuariolog request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorCategoria(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorCategoria(global::Usuariolog request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPorCategoria, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorTitulo(global::Usuariolog request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorTitulo(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorTitulo(global::Usuariolog request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPorTitulo, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorTiempo(global::tiempo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorTiempo(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorTiempo(global::tiempo request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPorTiempo, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorIngredientes(global::Usuariolog request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorIngredientes(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPorIngredientes(global::Usuariolog request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPorIngredientes, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPopulares(global::NuloReceta request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPopulares(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncServerStreamingCall<global::Receta> TraerRecetasPopulares(global::NuloReceta request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncServerStreamingCall(__Method_TraerRecetasPopulares, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::NuloReceta AgregarPuntuacion(global::Puntua request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return AgregarPuntuacion(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::NuloReceta AgregarPuntuacion(global::Puntua request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_AgregarPuntuacion, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::NuloReceta> AgregarPuntuacionAsync(global::Puntua request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return AgregarPuntuacionAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::NuloReceta> AgregarPuntuacionAsync(global::Puntua request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_AgregarPuntuacion, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Prommmm TraerPromedioPuntuacion(global::RecetaId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerPromedioPuntuacion(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Prommmm TraerPromedioPuntuacion(global::RecetaId request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_TraerPromedioPuntuacion, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Prommmm> TraerPromedioPuntuacionAsync(global::RecetaId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerPromedioPuntuacionAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Prommmm> TraerPromedioPuntuacionAsync(global::RecetaId request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_TraerPromedioPuntuacion, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Receta TraerRecetasPorTituloyUsuario(global::doble request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorTituloyUsuario(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Receta TraerRecetasPorTituloyUsuario(global::doble request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_TraerRecetasPorTituloyUsuario, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Receta> TraerRecetasPorTituloyUsuarioAsync(global::doble request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return TraerRecetasPorTituloyUsuarioAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Receta> TraerRecetasPorTituloyUsuarioAsync(global::doble request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_TraerRecetasPorTituloyUsuario, null, options, request);
    }
    /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected override RecetasClient NewInstance(ClientBaseConfiguration configuration)
    {
      return new RecetasClient(configuration);
    }
  }

  /// <summary>Creates service definition that can be registered with a server</summary>
  /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  public static grpc::ServerServiceDefinition BindService(RecetasBase serviceImpl)
  {
    return grpc::ServerServiceDefinition.CreateBuilder()
        .AddMethod(__Method_AltaReceta, serviceImpl.AltaReceta)
        .AddMethod(__Method_EditarReceta, serviceImpl.EditarReceta)
        .AddMethod(__Method_TraerRecetas, serviceImpl.TraerRecetas)
        .AddMethod(__Method_TraerRecetaPorId, serviceImpl.TraerRecetaPorId)
        .AddMethod(__Method_TraerRecetasPorUsuario, serviceImpl.TraerRecetasPorUsuario)
        .AddMethod(__Method_TraerRecetasPorCategoria, serviceImpl.TraerRecetasPorCategoria)
        .AddMethod(__Method_TraerRecetasPorTitulo, serviceImpl.TraerRecetasPorTitulo)
        .AddMethod(__Method_TraerRecetasPorTiempo, serviceImpl.TraerRecetasPorTiempo)
        .AddMethod(__Method_TraerRecetasPorIngredientes, serviceImpl.TraerRecetasPorIngredientes)
        .AddMethod(__Method_TraerRecetasPopulares, serviceImpl.TraerRecetasPopulares)
        .AddMethod(__Method_AgregarPuntuacion, serviceImpl.AgregarPuntuacion)
        .AddMethod(__Method_TraerPromedioPuntuacion, serviceImpl.TraerPromedioPuntuacion)
        .AddMethod(__Method_TraerRecetasPorTituloyUsuario, serviceImpl.TraerRecetasPorTituloyUsuario).Build();
  }

  /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
  /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
  /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
  /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  public static void BindService(grpc::ServiceBinderBase serviceBinder, RecetasBase serviceImpl)
  {
    serviceBinder.AddMethod(__Method_AltaReceta, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Receta, global::Responsea>(serviceImpl.AltaReceta));
    serviceBinder.AddMethod(__Method_EditarReceta, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::RecetaEditar, global::Responsea>(serviceImpl.EditarReceta));
    serviceBinder.AddMethod(__Method_TraerRecetas, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::NuloReceta, global::Receta>(serviceImpl.TraerRecetas));
    serviceBinder.AddMethod(__Method_TraerRecetaPorId, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::RecetaId, global::Receta>(serviceImpl.TraerRecetaPorId));
    serviceBinder.AddMethod(__Method_TraerRecetasPorUsuario, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Usuariolog, global::Receta>(serviceImpl.TraerRecetasPorUsuario));
    serviceBinder.AddMethod(__Method_TraerRecetasPorCategoria, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Usuariolog, global::Receta>(serviceImpl.TraerRecetasPorCategoria));
    serviceBinder.AddMethod(__Method_TraerRecetasPorTitulo, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Usuariolog, global::Receta>(serviceImpl.TraerRecetasPorTitulo));
    serviceBinder.AddMethod(__Method_TraerRecetasPorTiempo, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::tiempo, global::Receta>(serviceImpl.TraerRecetasPorTiempo));
    serviceBinder.AddMethod(__Method_TraerRecetasPorIngredientes, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Usuariolog, global::Receta>(serviceImpl.TraerRecetasPorIngredientes));
    serviceBinder.AddMethod(__Method_TraerRecetasPopulares, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::NuloReceta, global::Receta>(serviceImpl.TraerRecetasPopulares));
    serviceBinder.AddMethod(__Method_AgregarPuntuacion, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Puntua, global::NuloReceta>(serviceImpl.AgregarPuntuacion));
    serviceBinder.AddMethod(__Method_TraerPromedioPuntuacion, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::RecetaId, global::Prommmm>(serviceImpl.TraerPromedioPuntuacion));
    serviceBinder.AddMethod(__Method_TraerRecetasPorTituloyUsuario, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::doble, global::Receta>(serviceImpl.TraerRecetasPorTituloyUsuario));
  }

}
#endregion
