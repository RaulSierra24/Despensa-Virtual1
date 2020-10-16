using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace despensa.Models
{
    public partial class despensa1Context : DbContext
    {
        public despensa1Context()
        {
        }

        public despensa1Context(DbContextOptions<despensa1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<EstadoActividad> EstadoActividad { get; set; }
        public virtual DbSet<EstadoPedido> EstadoPedido { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<PredidoFactura> PredidoFactura { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=password;database=despensa1", x => x.ServerVersion("5.7.28-mysql"));
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

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
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

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.CodFactura)
                    .HasName("PRIMARY");

                entity.ToTable("factura");

                entity.HasIndex(e => e.CodCliente)
                    .HasName("factura_usuario_cliente_idx");

                entity.HasIndex(e => e.CodEmpleado)
                    .HasName("factura_empleados_idx");

                entity.Property(e => e.CodFactura)
                    .HasColumnName("cod_factura")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CodCliente)
                    .HasColumnName("cod_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEmpleado)
                    .HasColumnName("cod_empleado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.FacturaCodClienteNavigation)
                    .HasForeignKey(d => d.CodCliente)
                    .HasConstraintName("factura_usuario_cliente");

                entity.HasOne(d => d.CodEmpleadoNavigation)
                    .WithMany(p => p.FacturaCodEmpleadoNavigation)
                    .HasForeignKey(d => d.CodEmpleado)
                    .HasConstraintName("factura_usuario_empleados");
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
                    .HasName("pedido_cliente_idx");

                entity.HasIndex(e => e.CodEmpleado)
                    .HasName("pedido_empleado_idx");

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
                    .WithMany(p => p.PredidoFacturaCodClienteNavigation)
                    .HasForeignKey(d => d.CodCliente)
                    .HasConstraintName("pedido_cliente");

                entity.HasOne(d => d.CodEmpleadoNavigation)
                    .WithMany(p => p.PredidoFacturaCodEmpleadoNavigation)
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
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

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

                entity.HasIndex(e => e.CodEmpleado)
                    .HasName("puesto_empleado_idx");

                entity.Property(e => e.CodPuesto)
                    .HasColumnName("cod_puesto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEmpleado)
                    .HasColumnName("cod_empleado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Puesto1)
                    .HasColumnName("puesto")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CodEmpleadoNavigation)
                    .WithMany(p => p.Puesto)
                    .HasForeignKey(d => d.CodEmpleado)
                    .HasConstraintName("puesto_empleado");
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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CodUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.CodEstado)
                    .HasName("usuario_estado_idx");

                entity.HasIndex(e => e.CodGeneo)
                    .HasName("usuario_genero_idx");

                entity.HasIndex(e => e.CodRol)
                    .HasName("usuario_rol_idx");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("cod_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodEstado)
                    .HasColumnName("cod_estado")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.CodGeneo)
                    .HasColumnName("cod_geneo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

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

                entity.Property(e => e.FecNac)
                    .HasColumnName("fec_nac")
                    .HasColumnType("date");

                entity.Property(e => e.Nit)
                    .HasColumnName("nit")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'C/F'")
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
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("usuario_estado");

                entity.HasOne(d => d.CodGeneoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.CodGeneo)
                    .HasConstraintName("usuario_generos");

                entity.HasOne(d => d.CodRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.CodRol)
                    .HasConstraintName("usuario_rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
