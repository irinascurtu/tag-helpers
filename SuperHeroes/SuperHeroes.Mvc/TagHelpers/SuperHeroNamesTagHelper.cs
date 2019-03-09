using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SuperHeroese.Data;
using System.Linq;

namespace SuperHeroes.Mvc.TagHelpers
{
    ///<summary>
    /// Renders a select with superheroes
    /// </summary>
    [HtmlTargetElement("super-hero")]
    public class SuperHeroNamesTagHelper : TagHelper
    {
        private readonly ISuperHeroesRepository superHeroesRepository;

        public SuperHeroNamesTagHelper(ISuperHeroesRepository superHeroesRepository)
        {
            this.superHeroesRepository = superHeroesRepository;
        }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.SetAttribute("class", "awesome-select form-control");

            var names = superHeroesRepository
                        .GetHeroNamesAndIds()
                        .OrderBy(x => x.Nickname);

            foreach (var name in names)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };
                option.Attributes.Add("value", name.Id.ToString());
                option.Attributes.Add("class", name.Id.ToString());
                option.InnerHtml.Append(name.Nickname);
                output.Content.AppendHtml(option);
            }

        }

    }
}
