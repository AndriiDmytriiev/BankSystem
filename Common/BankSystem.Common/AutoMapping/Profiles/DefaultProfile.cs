namespace BankSystem.Common.AutoMapping.Profiles
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Interfaces;

    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            this.ConfigureMapping();
        }

        private void ConfigureMapping()
        {

           
/* CentralApi sollte in einer separaten SLN - Datei extrahiert werden oder zumindest keine Logik mit BankSystem.Common teilen. 
 * Wenn wir die benötigten Assemblys nicht exakt angeben, können unsere Tests fehlschlagen, 
 * da beim Start nicht alle Assemblys geladen werden, da Assemblys in .NET verzögert geladen werden.*/
            const string centralApiNamespace = "CentralApi";
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().FullName.Contains(nameof(BankSystem)) ||
                            a.GetName().FullName.Contains(centralApiNamespace))
                .SelectMany(a => a.GetTypes())
                .ToArray();

            var withBidirectionalMapping = allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.GetInterfaces()
                                .Where(i => i.IsGenericType)
                                .Select(i => i.GetGenericTypeDefinition())
                                .Contains(typeof(IMapWith<>)))
                .SelectMany(t =>
                    t.GetInterfaces()
                        .Where(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == typeof(IMapWith<>))
                        .SelectMany(i => i.GetGenericArguments())
                        .Select(s => new
                        {
                            Type1 = t,
                            Type2 = s
                        })
                )
                .ToArray();

            //Erstellen Sie eine bidirektionale Zuordnung für alle Typen, die die IMapWith<TModel>-Schnittstelle implementieren.
            foreach (var mapping in withBidirectionalMapping)
            {
                this.CreateMap(mapping.Type1, mapping.Type2);
                this.CreateMap(mapping.Type2, mapping.Type1);
            }

            // Erstellen Sie benutzerdefinierte Zuordnungen für alle Typen, die die IHaveCustomMapping-Schnittstelle implementieren.
            var customMappings = allTypes.Where(t => t.IsClass
                                                     && !t.IsAbstract
                                                     && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToArray();

            foreach (var mapping in customMappings)
            {
                mapping.ConfigureMapping(this);
            }
        }
    }
}

