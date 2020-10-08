using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace RecipeScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Klockan är 03:35, måste bara få detta att funka till imorgon.
            var lsit = new List<Recipe>();
            try
            {
                using (var b = new MemoryStream())
                {
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(@"https://www.koket.se/smorstekt-torskrygg-med-pestoslungad-blomkal-och-sparris");
                        using (var response = request.GetResponse())
                        {
                            var recepie = new Recipe();
                            var htmlDocument = new HtmlDocument();

                            using (var content = response.GetResponseStream())
                            {
                                using (var buffer = new MemoryStream())
                                {
                                    content.CopyTo(buffer);
                                    buffer.Seek(0, SeekOrigin.Begin);
                                    htmlDocument.Load(buffer);

                                    var htmlElement = htmlDocument.GetElementbyId("__NEXT_DATA__").InnerHtml;
                                    var structureData = JsonConvert.DeserializeObject<JObject>(htmlElement);

                                    var recepieData = structureData.SelectToken("props.pageProps.structuredData[?(@['\x40type']=='Recipe')]") as JObject;
                                    
                                    recepie.Name = recepieData["name"].ToObject<string>();
                                    recepie.Description = recepieData["description"].ToObject<string>();
                                    recepie.Image = recepieData["image"].ToObject<string>();

                                    recepie.Ingredient = new List<Ingredient>();
                                    foreach (var ingredient in recepieData["recipeIngredient"] as JArray)
                                    {
                                        var ingredientData = ((string)ingredient).Split(' ');
                                        var amountData = ingredientData[0];

                                        Ingredient ingredientToAdd;

                                        if (double.TryParse(amountData, out var amount))
                                        {
                                            var name = ingredientData.Skip(2);
                                            ingredientToAdd = new Ingredient 
                                            {
                                                Amount = amount,
                                                Unit = ingredientData[1],
                                                Name = string.Join(" ", name)
                                            };

                                            recepie.Ingredient.Add(ingredientToAdd);
                                        }
                                        else
                                        {

                                            ingredientToAdd = new Ingredient { Name = (string)ingredient };

                                        }
                                        
                                            
                                            recepie.Ingredient.Add(new Ingredient { Name = (string)ingredient });
                                        
                                    }
                                    recepie.Step = new List<Step>();

                                    foreach (var t4mp in recepieData["recipeInstructions"] as JArray)
                                    {
                                        var stripHtml = new Regex("<[^>]*(>|$)");
                                        
                                        var instructionalText = stripHtml.Replace(instructionalHtml, string.Empty);

                                        var stepToAdd = new Step
                                        {

                                            stripHtml = { instructionalText.Replace((string)t4mp["text"], string.Empty).Replace("\n", " ") }

                                        };
                                    }
                                        recepie.Step.Add 
                                };

                                lsit.Add(recepie);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                }

                try
                {
                    var r = (HttpWebRequest)WebRequest.Create(@"https://www.koket.se/vegoburgare-med-tryffelmajonnas");
                    using (var rr = r.GetResponse())
                    {
                        var tmp = new Recipe();
                        var htd = new HtmlDocument();
                        using (var c = rr.GetResponseStream())
                        {
                            using (var tmp2 = new MemoryStream())
                            {
                                c.CopyTo(tmp2);
                                tmp2.Seek(0, SeekOrigin.Begin);
                                htd.Load(tmp2);
                                var d = JsonConvert.DeserializeObject<JObject>(htd.GetElementbyId("__NEXT_DATA__").InnerHtml)
                                                                .SelectToken("props.pageProps.structuredData[?(@['\x40type']=='Recipe')]") as JObject;
                                tmp.Name = (string)d["name"].ToObject(typeof(string));
                                tmp.Description = (string)d["description"].ToObject(typeof(string));
                                tmp.Image = (string)d["image"].ToObject(typeof(string));
                                tmp.Ingredient = new List<Ingredient>();
                                foreach (var tm3p in d["recipeIngredient"] as JArray)
                                {
                                    if (double.TryParse(((string)tm3p).Split(' ')[0], out var a))
                                    {
                                        var n = ((string)tm3p).Split(' ').Skip(2);
                                        tmp.Ingredient.Add(new Ingredient { Amount = a, Unit = ((string)tm3p).Split(' ')[1], Name = string.Join(" ", n) });
                                    }
                                    else
                                    {
                                        tmp.Ingredient.Add(new Ingredient { Name = (string)tm3p });
                                    }
                                }
                                tmp.Step = new List<Step>();
                                foreach (var t4mp in d["recipeInstructions"] as JArray)
                                    tmp.Step.Add(new Step { Tx = new Regex("<[^>]*(>|$)").Replace((string)t4mp["text"], string.Empty).Replace("\n", " ") });
                            }
                            lsit.Add(tmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                try
                {
                    var r = (HttpWebRequest)WebRequest.Create(@"https://www.koket.se/smakrik-o-lrisotto-med-vitlo-ksrostad-tomat-citronsparris-och-het-chorizo");
                    using (var rr = r.GetResponse())
                    {
                        var tmp = new Recipe();
                        var htd = new HtmlDocument();
                        using (var c = rr.GetResponseStream())
                        {
                            using (var tmp2 = new MemoryStream())
                            {
                                c.CopyTo(tmp2);
                                tmp2.Seek(0, SeekOrigin.Begin);
                                htd.Load(tmp2);
                                var d = JsonConvert.DeserializeObject<JObject>(htd.GetElementbyId("__NEXT_DATA__").InnerHtml)
                                .SelectToken("props.pageProps.structuredData[?(@['\x40type']=='Recipe')]") as JObject;
                                tmp.Name = (string)d["name"].ToObject(typeof(string));
                                tmp.Description = (string)d["description"].ToObject(typeof(string));
                                tmp.Image = (string)d["image"].ToObject(typeof(string));
                                tmp.Ingredient = new List<Ingredient>();
                                foreach (var tm3p in d["recipeIngredient"] as JArray)
                                {
                                    if (double.TryParse(((string)tm3p).Split(' ')[0], out var a))
                                    {
                                        var n = ((string)tm3p).Split(' ').Skip(2);
                                        tmp.Ingredient.Add(new Ingredient { Amount = a, Unit = ((string)tm3p).Split(' ')[1], Name = string.Join(" ", n) });
                                    }
                                    else
                                    {
                                        tmp.Ingredient.Add(new Ingredient { Name = (string)tm3p });
                                    }
                                }
                                tmp.Step = new List<Step>();
                                foreach (var t4mp in d["recipeInstructions"] as JArray)
                                    tmp.Step.Add(new Step { Tx = new Regex("<[^>]*(>|$)").Replace((string)t4mp["text"], string.Empty).Replace("\n", " ") });
                            }

                            lsit.Add(tmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                try
                {
                    var r = (HttpWebRequest)WebRequest.Create(@"https://www.koket.se/smorbakad-spetskal-med-krispig-kyckling-och-graddskum");
                    using (var rr = r.GetResponse())
                    {
                        var tmp = new Recipe();
                        var htd = new HtmlDocument();
                        using (var c = rr.GetResponseStream())
                        {
                            using (var tmp2 = new MemoryStream())
                            {
                                c.CopyTo(tmp2);
                                tmp2.Seek(0, SeekOrigin.Begin);
                                htd.Load(tmp2);
                                var d = JsonConvert.DeserializeObject<JObject>(htd.GetElementbyId("__NEXT_DATA__").InnerHtml)
                                .SelectToken("props.pageProps.structuredData[?(@['\x40type']=='Recipe')]") as JObject;
                                tmp.Name = (string)d["name"].ToObject(typeof(string));
                                tmp.Description = (string)d["description"].ToObject(typeof(string));
                                tmp.Image = (string)d["image"].ToObject(typeof(string));
                                tmp.Ingredient = new List<Ingredient>();
                                foreach (var tm3p in d["recipeIngredient"] as JArray)
                                {
                                    if (double.TryParse(((string)tm3p).Split(' ')[0], out var a))
                                    {
                                        var n = ((string)tm3p).Split(' ').Skip(2);
                                        tmp.Ingredient.Add(new Ingredient { Amount = a, Unit = ((string)tm3p).Split(' ')[1], Name = string.Join(" ", n) });
                                    }
                                    else
                                    {
                                        tmp.Ingredient.Add(new Ingredient { Name = (string)tm3p });
                                    }
                                }
                                tmp.Step = new List<Step>();
                                foreach (var t4mp in d["recipeInstructions"] as JArray)
                                    tmp.Step.Add(new Step { Tx = new Regex("<[^>]*(>|$)").Replace((string)t4mp["text"], string.Empty).Replace("\n", " ") });
                            }

                            lsit.Add(tmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            try
            {
                for (var x = 0; x < lsit.Count; x++)
                {
                    Console.WriteLine(@$"Recept:" + " " + lsit[x].Name);
                    Console.WriteLine("-------");
                    Console.WriteLine(lsit[x].Description.Trim());
                    Console.WriteLine("-------");
                    Console.WriteLine("Ingredienser:");
                    

                    for (var y = 0; y < lsit[x].Ingredient.Count; y++)
                    {
                        if (lsit[x].Ingredient[y].Amount == 0)
                            Console.WriteLine(lsit[x].Ingredient[y].Name);
                        else
                            Console.WriteLine(lsit[x].Ingredient[y].Amount.ToString() + " " + lsit[x].Ingredient[y].Unit + " " + lsit[x].Ingredient[y].Name);
                    }
                    Console.WriteLine("-------");
                    Console.WriteLine("Steg:");

                    var stepCount = 1;

                    for (var z = 0; z < lsit[x].Step.Count; z++)
                    {
                        Console.WriteLine((z + 1).ToString() + ": " + lsit[x].Step[z].Tx);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredient { get; set; }
        public List<Step> Step { get; set; }
    }

    class Ingredient
    {
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }

    class Step
    {
        public string T1 { get; set; }
        public string Tx { get; set; }
    }
}
