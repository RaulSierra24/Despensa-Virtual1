using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace despensa.Models
{
    public partial class despensaContext : DbContext
    {
        public despensaContext()
        {
        }

        public despensaContext(DbContextOptions<despensaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EstadoActividad> EstadoActividad { get; set; }
        public virtual DbSet<EstadoPedido> EstadoPedido { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<PredidoFactura> PredidoFactura { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=192.168.99.100;port=3306;user=root;password=password;database=despensa", x => x.ServerVersion("5.7.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.CodBitacora)
                    .HasName("PRIMARY");

                entity.ToTable("bitacora");

                entity.Property(e => e.CodBitacora)
                    .HasColumnName("cod_bitacora")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Accion)
                    .HasColumnName("accion")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Empleado)
                    .HasColumnName("empleado")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.CodCategoria)
                    .HasName("PRIMARY");

                entity.ToTable("categoria");

                entity.HasIndex(e => e.Estado)
                    .HasName("estado_inx");

                entity.Property(e => e.CodCategoria)
                    .HasColumnName("cod_categoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasColumnType("varchar(450)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Categoria)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("categoria_estado");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCliente)
                    .HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("estado_index");

                entity.HasIndex(e => e.CodGenero)
                    .HasName("genero_index");

                entity.HasIndex(e => e.CodRol)
                    .HasName("rol_index");

                entity.Property(e => e.CodCliente)
                    .HasColumnName("cod_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contrasenia)
                    .HasColumnName("contrasenia")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CorreoElectronico)
                    .HasColumnName("correo_electronico")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Cui)
                    .HasColumnName("cui")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnName("fec_nacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nit)
                    .HasColumnName("nit")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PrimerApellido)
                    .HasColumnName("primer_apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PrimerNombre)
                    .HasColumnName("primer_nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SegundoApellido)
                    .HasColumnName("segundo_apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SegundoNombre)
                    .HasColumnName("segundo_nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("cod_estado");

                entity.HasOne(d => d.CodGeneroNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.CodGenero)
                    .HasConstraintName("cliente_genero");

                entity.HasOne(d => d.CodRolNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.CodRol)
                    .HasConstraintName("cliente_rol");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.CodComentario)
                    .HasName("PRIMARY");

                entity.ToTable("comentario");

                entity.HasIndex(e => e.CodCliente)
                    .HasName("comentario_cliente_idx");

                entity.Property(e => e.CodComentario)
                    .HasColumnName("cod_comentario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodCliente)
                    .HasColumnName("cod_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comentario1)
                    .HasColumnName("comentario")
                    .HasColumnType("varchar(4500)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.CodCliente)
                    .HasConstraintName("comentario_cliente");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.CodDetalle)
                    .HasName("PRIMARY");

                entity.ToTable("detalle_factura");

                entity.HasIndex(e => e.CodFactura)
                    .HasName("detalle_fac_factura_idx");

                entity.HasIndex(e => e.CodProducto)
                    .HasName("detalle_fac_producto_idx");

                entity.Property(e => e.CodDetalle)
                    .HasColumnName("cod_detalle")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodFactura)
                    .HasColumnName("cod_factura")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodProducto)
                    .HasColumnName("cod_producto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Costo)
                    .HasColumnName("costo")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.CodFacturaNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.CodFactura)
                    .HasConstraintName("detalle_fac_factura");

                entity.HasOne(d => d.CodProductoNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.CodProducto)
                    .HasConstraintName("detalle_fac_producto");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.CodEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("empleado");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("estado_index");

                entity.HasIndex(e => e.CodGenero)
                    .HasName("genero_index");

                entity.HasIndex(e => e.CodPuesto)
                    .HasName("puesto_index");

                entity.HasIndex(e => e.CodRol)
                    .HasName("rol_index");

                entity.Property(e => e.CodEmpleado)
                    .HasColumnName("cod_empleado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodPuesto)
                    .HasColumnName("cod_puesto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contraseña)
                    .HasColumnName("contraseña")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CorreoElectronico)
                    .HasColumnName("correo_electronico")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Cui)
                    .HasColumnName("cui")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrimerApellido)
                    .HasColumnName("primer_apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PrimerNombre)
                    .HasColumnName("primer_nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SegundoApellido)
                    .HasColumnName("segundo_apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SegundoNombre)
                    .HasColumnName("segundo_nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("empleado_estado");

                entity.HasOne(d => d.CodGeneroNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.CodGenero)
                    .HasConstraintName("empleado_genero");

                entity.HasOne(d => d.CodPuestoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.CodPuesto)
                    .HasConstraintName("empleado_puesto");

                entity.HasOne(d => d.CodRolNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.CodRol)
                    .HasConstraintName("empleado_rol");
            });

            modelBuilder.Entity<EstadoActividad>(entity =>
            {
                entity.HasKey(e => e.CodEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_actividad");

                entity.HasComment("	");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.CodEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_pedido");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.CodGenero)
                    .HasName("PRIMARY");

                entity.ToTable("genero");

                entity.HasComment("	");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Genero1)
                    .HasColumnName("genero")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.CodMarca)
                    .HasName("PRIMARY");

                entity.ToTable("marca");

                entity.Property(e => e.CodMarca)
                    .HasColumnName("cod_marca")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Marca1)
                    .HasColumnName("marca")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<PredidoFactura>(entity =>
            {
                entity.HasKey(e => e.CodFactura)
                    .HasName("PRIMARY");

                entity.ToTable("predido_factura");

                entity.HasIndex(e => e.CodCliente)
                    .HasName("cliente_idx");

                entity.HasIndex(e => e.CodEmpleado)
                    .HasName("empleado_idx");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("estado_idx");

                entity.Property(e => e.CodFactura)
                    .HasColumnName("cod_factura")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodCliente)
                    .HasColumnName("cod_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEmpleado)
                    .HasColumnName("cod_empleado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FecEmision)
                    .HasColumnName("fec_emision")
                    .HasColumnType("datetime");

                entity.Property(e => e.TotalCosto)
                    .HasColumnName("total_costo")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.TotalVendido)
                    .HasColumnName("total_vendido")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.PredidoFactura)
                    .HasForeignKey(d => d.CodCliente)
                    .HasConstraintName("pedido_cliente");

                entity.HasOne(d => d.CodEmpleadoNavigation)
                    .WithMany(p => p.PredidoFactura)
                    .HasForeignKey(d => d.CodEmpleado)
                    .HasConstraintName("pedido_empleado");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.PredidoFactura)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("pedido_estado");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.CodProducto)
                    .HasName("PRIMARY");

                entity.ToTable("producto");

                entity.HasIndex(e => e.CodCategoria)
                    .HasName("producto_categoria_idx");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("estado_idx");

                entity.HasIndex(e => e.CodMarca)
                    .HasName("marca_idx");

                entity.HasIndex(e => e.CodProveedor)
                    .HasName("producto_proveedor_idx");

                entity.Property(e => e.CodProducto)
                    .HasColumnName("cod_producto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodCategoria)
                    .HasColumnName("cod_categoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodMarca)
                    .HasColumnName("cod_marca")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodProveedor)
                    .HasColumnName("cod_proveedor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FecCaducidad)
                    .HasColumnName("fec_caducidad")
                    .HasColumnType("datetime");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.PrecioCosto)
                    .HasColumnName("precio_costo")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnName("precio_venta")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.CodCategoria)
                    .HasConstraintName("producto_categoria");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("producto_estado");

                entity.HasOne(d => d.CodMarcaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.CodMarca)
                    .HasConstraintName("producto_marca");

                entity.HasOne(d => d.CodProveedorNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.CodProveedor)
                    .HasConstraintName("producto_proveedor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.CodProveedor)
                    .HasName("PRIMARY");

                entity.ToTable("proveedor");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("proveedor_idx");

                entity.Property(e => e.CodProveedor)
                    .HasColumnName("cod_proveedor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Proveedor1)
                    .HasColumnName("proveedor")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("proveedor");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.CodPuesto)
                    .HasName("PRIMARY");

                entity.ToTable("puesto");

                entity.Property(e => e.CodPuesto)
                    .HasColumnName("cod_puesto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Puesto1)
                    .HasColumnName("puesto")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.CodRol)
                    .HasName("PRIMARY");

                entity.ToTable("rol");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rol1)
                    .HasColumnName("rol")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
