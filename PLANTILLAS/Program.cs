using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Collections.Generic;
using System.IO;

public class DetalleOdontograma
{
    public string Pieza { get; set; }
    public string Estado { get; set; }
    public string Observacion { get; set; }
    public string Tratamiento { get; set; }
    public string Superficie { get; set; }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("pdf");

        Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(50);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));
                var rutaImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Img", "logo.png");
                var rutaImgOdon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Img", "Odon.png");
                byte[] imageData = File.ReadAllBytes(rutaImg);
                byte[] imageDataOdon = File.ReadAllBytes(rutaImgOdon);

                page.Header().ShowOnce().Height(80).Row(row =>
                {
                    row.ConstantColumn(150).Height(60).Image(imageData).FitArea();
                    row.RelativeItem().Column(col => {
                        col.Item().PaddingTop(10).AlignCenter().Text("ODONTOGRAMA").Bold().FontSize(16);
                    });
                    row.RelativeItem().Column(col => {
                        col.Item().AlignRight().Text("Av. Japon, 3er. Anillo externo");
                        col.Item().AlignRight().Text("(frente al Hospital Japones)");
                        col.Item().AlignRight().Text("Celular: 62121666");
                    });
                });

                page.Content().Column(col => {
                    col.Item().PaddingBottom(5).LineHorizontal(0.5f);
                    col.Item().Row(row => {
                        row.RelativeItem().Column(col => {
                            col.Item().Text(txt => {
                                txt.Span("CÓDIGO HC:  ").SemiBold();
                                txt.Span("HC-0001");
                            });
                        });
                        row.RelativeItem().Column(col => {
                            col.Item().Text(txt => {
                                txt.Span("FECHA:  ").SemiBold();
                                txt.Span("24/04/1999");
                            });
                        });
                    });
                    col.Item().Row(row => {
                        row.RelativeItem().Column(col => {
                            col.Item().Text(txt => {
                                txt.Span("PACIENTE:  ").SemiBold();
                                txt.Span("MARIO LOPEZ HOBRADOR");
                            });
                        });
                    });
                    col.Item().PaddingTop(5).PaddingBottom(15).LineHorizontal(0.5f);

                    // Mostrar imagen del odontograma
                    col.Item().Image(imageDataOdon).FitArea();

                    col.Item().PaddingTop(15).PaddingBottom(5).LineHorizontal(0.2f);

                    // Generar observación aleatoria
                    var observacionesGenerales = new List<string>
                    {
                        "El paciente presenta caries en varias piezas dentales.",
                        "Se observan encías inflamadas en el cuadrante superior derecho.",
                        "El paciente reporta dolor al masticar en el lado izquierdo.",
                        "Se recomienda realizar una limpieza dental profunda.",
                        "Presencia de sarro en la mayoría de las piezas dentales.",
                        "Necesidad de tratamiento de conducto en pieza 36."
                    };
                    var observacionAleatoria = observacionesGenerales[new Random().Next(observacionesGenerales.Count)];

                    col.Item().Text("Observaciones:").SemiBold();
                    col.Item().Padding(5).Text(observacionAleatoria);

                    col.Item().AlignLeft().Padding(5).Text("DETALLE").SemiBold().FontSize(11);
                    col.Item().Table(tabla => {
                        tabla.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(100);
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(150);
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(100);
                        });
                        tabla.Header(header => {
                            header.Cell().Padding(2).Text("Pieza").SemiBold();
                            header.Cell().Padding(2).Text("Estado").SemiBold();
                            header.Cell().Padding(2).Text("Observacion").SemiBold();
                            header.Cell().Padding(2).Text("Tratamiento").SemiBold();
                            header.Cell().Padding(2).Text("Superfice").SemiBold();
                        });

                        var detalles = new List<DetalleOdontograma>
                        {
                            new DetalleOdontograma { Pieza = "11", Estado = "Caries", Observacion = "Necesita tratamiento", Tratamiento = "Empaste", Superficie = "Vestibular" },
                            new DetalleOdontograma { Pieza = "12", Estado = "Sano", Observacion = "Ninguna", Tratamiento = "Ninguno", Superficie = "Lingual" },
                            new DetalleOdontograma { Pieza = "13", Estado = "Fractura", Observacion = "Urgente", Tratamiento = "Reparación", Superficie = "Mesial" },
                            new DetalleOdontograma { Pieza = "14", Estado = "Sarro", Observacion = "Limpieza", Tratamiento = "Profilaxis", Superficie = "Distal" },
                            new DetalleOdontograma { Pieza = "15", Estado = "Caries", Observacion = "Tratamiento", Tratamiento = "Empaste", Superficie = "Oclusal" },
                            new DetalleOdontograma { Pieza = "16", Estado = "Ausente", Observacion = "Implante", Tratamiento = "Implante", Superficie = "N/A" }
                        };

                        foreach (var detalle in detalles)
                        {
                            tabla.Cell().Padding(2).Text(detalle.Pieza);
                            tabla.Cell().Padding(2).Text(detalle.Estado);
                            tabla.Cell().Padding(2).Text(detalle.Observacion);
                            tabla.Cell().Padding(2).Text(detalle.Tratamiento);
                            tabla.Cell().Padding(2).Text(detalle.Superficie);
                        }
                    });
                    col.Item().PaddingTop(10).LineHorizontal(0.2f);

                    col.Item().Height(70);
                    col.Item().AlignCenter().Text("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                    col.Item().AlignCenter().Text("Dr(a). Juan Perez").FontSize(12);
                });

                page.Footer().AlignRight().Text(txt => {
                    txt.Span("Página ");
                    txt.CurrentPageNumber();
                    txt.Span(" - ");
                    txt.TotalPages();
                });
            });
        }).ShowInPreviewer();
    }
}
