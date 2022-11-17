﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace NN_Inmuebles.Migrations
{
    [DbContext(typeof(NN_InmueblesContext))]
    [Migration("20221117023451_Complete")]
    partial class Complete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NN_Inmuebles.Models.Alquiler", b =>
                {
                    b.Property<int>("AlquilerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlquilerID"), 1L, 1);

                    b.Property<int>("CasaID")
                        .HasColumnType("int");

                    b.Property<string>("CasaNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<string>("ClienteNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlquiler")
                        .HasColumnType("datetime2");

                    b.HasKey("AlquilerID");

                    b.HasIndex("CasaID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Alquiler");
                });

            modelBuilder.Entity("NN_Inmuebles.Models.Casa", b =>
                {
                    b.Property<int>("CasaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CasaID"), 1L, 1);

                    b.Property<bool>("Alquilada")
                        .HasColumnType("bit");

                    b.Property<string>("Domicilio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminada")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ImagenCasa")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NombreCasa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDueño")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CasaID");

                    b.ToTable("Casa");
                });

            modelBuilder.Entity("NN_Inmuebles.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteID"), 1L, 1);

                    b.Property<string>("ApellidoCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreCliente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteID");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("NN_Inmuebles.Models.Devolucion", b =>
                {
                    b.Property<int>("DevolucionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DevolucionID"), 1L, 1);

                    b.Property<int>("AlquilerID")
                        .HasColumnType("int");

                    b.Property<int>("CasaID")
                        .HasColumnType("int");

                    b.Property<string>("CasaNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<string>("ClienteNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDevolucion")
                        .HasColumnType("datetime2");

                    b.HasKey("DevolucionID");

                    b.HasIndex("CasaID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Devolucion");
                });

            modelBuilder.Entity("NN_Inmuebles.Models.Alquiler", b =>
                {
                    b.HasOne("NN_Inmuebles.Models.Casa", "Casa")
                        .WithMany()
                        .HasForeignKey("CasaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NN_Inmuebles.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Casa");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("NN_Inmuebles.Models.Devolucion", b =>
                {
                    b.HasOne("NN_Inmuebles.Models.Casa", "Casa")
                        .WithMany()
                        .HasForeignKey("CasaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NN_Inmuebles.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Casa");

                    b.Navigation("Cliente");
                });
#pragma warning restore 612, 618
        }
    }
}
