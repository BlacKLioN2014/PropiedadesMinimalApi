using Microsoft.AspNetCore.Mvc;
using PropiedadesMinimalApi.Data;
using PropiedadesMinimalApi.Maps;
using PropiedadesMinimalApi.Models;
using PropiedadesMinimalApi.Models.DTO;
using AutoMapper;
using FluentValidation;
using System.Net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurar conexion a BD
builder.Services.AddDbContext<ApplicationDbContext>(Opciones => 
        Opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Añadir automapper
builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));

//Añadir validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

////Primeros Endpoints
//app.MapGet("/Obtener", () => "Bienvenidos GET");

//app.MapPost("/Crear", () => "Bienvenidos POST");

//app.MapPut("/Actualizar", () => "Bienvenidos PUT");

//app.MapGet("/Mensaje", () => 
//{
//    return "Bienvenidos GET 2";
//});

//app.MapGet("/Error", () =>
//{
//    return Results.BadRequest("Error generado");
//});

//app.MapGet("/Prueba/{Id:int}", (int Id) =>
//{
//    return Results.Ok($"EL id es: {Id}");
//});

////Obtener todas las propiedades -get-mapget
//app.MapGet("/api/propiedadesOne", () => Results.Ok(DatosPropiedad.listaPropiedades));



//Version sin base de datos
app.MapGet("/api/propiedadesDeLista", (ILogger<Program> logger) =>
{

    RespuestaApi respuesta = new RespuestaApi();

    //Usar el _logger que ya esta como inyeccion de dependencias
    logger.Log(LogLevel.Information, "Carga de todas las propiedades");

    respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedadesDeLista").Produces<RespuestaApi>(200);


//Version con base de datos
app.MapGet("/api/propiedadesDeBd", async (ApplicationDbContext _bd, ILogger<Program> logger) =>
{

    RespuestaApi respuesta = new RespuestaApi();

    //Usar el _logger que ya esta como inyeccion de dependencias
    logger.Log(LogLevel.Information, "Carga de todas las propiedades");

    respuesta.Resultado = _bd.Propiedad;
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedadesDeBd").Produces<RespuestaApi>(200);



//Version sin base de datos
app.MapGet("/api/Get_PropiedadDeLista/{Id:int}", (int Id) =>
{

    RespuestaApi respuesta = new RespuestaApi();

    respuesta.Resultado = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == Id);
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedadDeLista").Produces<RespuestaApi>(200);


//Version con base de datos
app.MapGet("/api/Get_PropiedadDeBd/{Id:int}", async (ApplicationDbContext _bd, int Id) =>
{

    RespuestaApi respuesta = new RespuestaApi();

    respuesta.Resultado = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == Id);
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedadDeBd").Produces<RespuestaApi>(200);



//Version sin base de datos
app.MapPost("/api/Add_PropiedadEnLista", async ( IMapper _mapper, IValidator<CrearPropiedadDTO> _validacion, [FromBody] CrearPropiedadDTO crearPropiedadDTO) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    //var resultadoValidaciones =  _validacion.ValidateAsync(crearPropiedadDTO).GetAwaiter().GetResult();
    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDTO);

    //validar id de propiedad y que el nombre no este vacio.
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());

        return Results.BadRequest(respuesta);
    }

    //Validar si el nombre de la propiedad ya existe,
    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("El nombre de la  propiedad ya existe");

        return Results.BadRequest(respuesta);
    }

    //Propiedad propiedad = new Propiedad() 
    //{
    //    Nombre = crearPropiedadDTO.Nombre,
    //    Descripcion = crearPropiedadDTO.Descripcion,
    //    Ubicacion = crearPropiedadDTO.Ubicacion,
    //    Activa = crearPropiedadDTO.Activa,
    //};
    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDTO);

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;

    //PropiedadDTO propiedadDTO = new PropiedadDTO()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa,
    //};
    PropiedadDTO propiedadDTO = _mapper.Map<PropiedadDTO>(propiedad);

    DatosPropiedad.listaPropiedades.Add(propiedad);

    //return Results.Created($"/api/Get_Propiedad/{propiedad.IdPropiedad}", propiedad);
    //return Results.CreatedAtRoute("ObtenerPropiedad", new { Id = propiedadDTO.IdPropiedad }, propiedadDTO);

    respuesta.Resultado = propiedadDTO;
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.Created;

    return Results.Ok(respuesta);

}).WithName("CrearPropiedadEnLista").Accepts<CrearPropiedadDTO>("application/json").Produces<RespuestaApi>(201).Produces(400);


//Version con base de datos 
app.MapPost("/api/Add_PropiedadEnBd", async (ApplicationDbContext _bd, IMapper _mapper, IValidator<CrearPropiedadDTO> _validacion, [FromBody] CrearPropiedadDTO crearPropiedadDTO) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDTO);

    //validar id de propiedad y que el nombre no este vacio.
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());

        return Results.BadRequest(respuesta);
    }

    //Validar si el nombre de la propiedad ya existe,
    if (await _bd.Propiedad.FirstOrDefaultAsync(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("El nombre de la  propiedad ya existe");

        return Results.BadRequest(respuesta);
    }

    //Propiedad propiedad = new Propiedad() 
    //{
    //    Nombre = crearPropiedadDTO.Nombre,
    //    Descripcion = crearPropiedadDTO.Descripcion,
    //    Ubicacion = crearPropiedadDTO.Ubicacion,
    //    Activa = crearPropiedadDTO.Activa,
    //};
    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDTO);
    propiedad.FechaCreacion =DateTime.Now;

    //PropiedadDTO propiedadDTO = new PropiedadDTO()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa,
    //};
    PropiedadDTO propiedadDTO = _mapper.Map<PropiedadDTO>(propiedad);

    await _bd.Propiedad.AddAsync(propiedad);
    await _bd.SaveChangesAsync();

    propiedadDTO.IdPropiedad = propiedad.IdPropiedad;

    respuesta.Resultado = propiedadDTO;
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.Created;

    return Results.Ok(respuesta);

}).WithName("CrearPropiedadEnBd").Accepts<CrearPropiedadDTO>("application/json").Produces<RespuestaApi>(201).Produces(400);



//Version sin base de datos
app.MapPut("/api/Actualizar_PropiedadEnLista", async (IMapper _mapper, IValidator<ActualizarPropiedadDTO> _validacion, [FromBody] ActualizarPropiedadDTO ActualizarPropiedadDTO) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    var resultadoValidaciones = await _validacion.ValidateAsync(ActualizarPropiedadDTO);

    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());

        return Results.BadRequest(respuesta);
    }

    Propiedad propiedadDesdeDb = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == ActualizarPropiedadDTO.IdPropiedad);

    propiedadDesdeDb.Nombre = ActualizarPropiedadDTO.Nombre;
    propiedadDesdeDb.Descripcion = ActualizarPropiedadDTO.Descripcion;
    propiedadDesdeDb.Ubicacion = ActualizarPropiedadDTO.Ubicacion;
    propiedadDesdeDb.Activa = ActualizarPropiedadDTO.Activa;

    respuesta.Resultado = _mapper.Map<PropiedadDTO>(propiedadDesdeDb);
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedadEnLista").Accepts<ActualizarPropiedadDTO>("application/json").Produces<RespuestaApi>(201).Produces(400);


//Version con base de datos
app.MapPut("/api/Actualizar_PropiedadEnBd", async (ApplicationDbContext _bd, IMapper _mapper, IValidator<ActualizarPropiedadDTO> _validacion, [FromBody] ActualizarPropiedadDTO ActualizarPropiedadDTO) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    var resultadoValidaciones = await _validacion.ValidateAsync(ActualizarPropiedadDTO);

    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());

        return Results.BadRequest(respuesta);
    }

    Propiedad propiedadDesdeDb = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == ActualizarPropiedadDTO.IdPropiedad);

    propiedadDesdeDb.Nombre = ActualizarPropiedadDTO.Nombre;
    propiedadDesdeDb.Descripcion = ActualizarPropiedadDTO.Descripcion;
    propiedadDesdeDb.Ubicacion = ActualizarPropiedadDTO.Ubicacion;
    propiedadDesdeDb.Activa = ActualizarPropiedadDTO.Activa;

    await _bd.SaveChangesAsync();

    respuesta.Resultado = _mapper.Map<PropiedadDTO>(propiedadDesdeDb);
    respuesta.Success = true;
    respuesta.Codigo_Estado = HttpStatusCode.OK;

    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedadEnBd").Accepts<ActualizarPropiedadDTO>("application/json").Produces<RespuestaApi>(201).Produces(400);



//Version sin base de datos
app.MapDelete("/api/Get_PropiedadEnLista/{Id:int}", (int Id) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    //Obtener el id de la propiedad a eliminar
    Propiedad propiedadDesdeDb = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == Id);

    if (propiedadDesdeDb != null)
    {
        DatosPropiedad.listaPropiedades.Remove(propiedadDesdeDb);
        respuesta.Success = true;
        respuesta.Codigo_Estado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else
    {
        respuesta.Errores.Add("El id de la propiedad es inválido");
        return Results.BadRequest(respuesta);
    }

});


//Version con base de datos
app.MapDelete("/api/Get_PropiedadEnBd/{Id:int}", async (ApplicationDbContext _bd, int Id) =>
{

    RespuestaApi respuesta = new() { Success = false, Codigo_Estado = HttpStatusCode.BadRequest };

    //Obtener el id de la propiedad a eliminar
    Propiedad propiedadDesdeDb = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == Id);

    if (propiedadDesdeDb != null)
    {
        _bd.Propiedad.Remove(propiedadDesdeDb);
        await _bd.SaveChangesAsync();

        respuesta.Success = true;
        respuesta.Codigo_Estado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else
    {
        respuesta.Errores.Add("El id de la propiedad es inválido");
        return Results.BadRequest(respuesta);
    }

});



app.Run();