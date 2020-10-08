using NGeoNames;
using NGeoNames.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;

namespace geocode
{
    class Program
    {
        static readonly IEnumerable<ExtendedGeoName> _locationNames;
        static readonly ReverseGeoCode<ExtendedGeoName> _reverseGeoCodingService;
        static readonly (double Lat, double Lng) _gavlePosition;
        static readonly (double Lat, double Lng) _uppsalaPosition;
        static readonly  (double Lat, double Lng) _formatProvider;

        static Program()
        {
            _locationNames = GeoFileReader.ReadExtendedGeoNames(".\\Resources\\SE.txt");
            _reverseGeoCodingService = new ReverseGeoCode<ExtendedGeoName>(_locationNames);
            _gavlePosition = (60.674622, 17.141830);
            //_gavleExcludeNames = new HashSet<string> { "Gävle" };
            _uppsalaPosition = (59.858562, 17.638927);

            _formatProvider = CultureInfo.InvariantCulture;

        }
        static void Main(string[] args)
        {
            // 1. Hitta de 10 närmsta platserna till Gävle (60.674622, 17.141830), sorterat på namn.
            Console.WriteLine("1. Gävle");
            Console.WriteLine("---------");
            
            ListGavlePosition();
            // 2. Hitta alla platser inom 200 km radie till Uppsala (59.858562, 17.638927), sorterat på avstånd.
           Console.WriteLine("2. Uppsala");
            Console.WriteLine("--------");
           
                ListUppsalaPositions();
            Console.WriteLine("3. User");
            Console.WriteLine("-------");

            ListUserPositions();


            // 3. Lista x platser baserat på användarinmatning.
            static void ListUserPositions();
            {
                double Lat = double.Parse(args[0],_formatProvider);
                double Lng = double.Parse(args[1],_formatProvider);

                var nearestUser 



            }
         
        }
        static void ListUppsalaPositions()
        {
            var radius = 200 * 1000;    
            var nearestUppsala = _reverseGeoCodingService.RadialSearch(_uppsalaPosition.Lat, _uppsalaPosition.Lng, radius, 50);

            nearestUppsala = nearestUppsala.OrderBy(x => x.DistanceTo(_uppsalaPosition.Lat, _uppsalaPosition.Lng));

            foreach (var position in nearestUppsala)
            {
                var uppsalaDistance = position.DistanceTo(_uppsalaPosition.Lat, _uppsalaPosition.Lng);

                Console.WriteLine($"{position.Name}, distance to Uppsala: {uppsalaDistance}");
            }

        }
             static void ListGavlePosition()
        {
            var nearestGavle = _reverseGeoCodingService.RadialSearch(_gavlePosition.Lat, _gavlePosition.Lng, 10);

            nearestGavle = nearestGavle.OrderBy(p => p.Name);

            foreach (var position in nearestGavle)
            {
                Console.WriteLine($"{position.Name}, lat: {position.Latitude}, lng: {position.Longitude}");
            }

            
        }
    }
}
