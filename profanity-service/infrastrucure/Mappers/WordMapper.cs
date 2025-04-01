using infrastrucure.Models;
using service.Models;

namespace infrastrucure.Mappers;

public static class WordMapper
{
    public static WordDbModel ToDbModel(this WordModel model)
    {
        return new WordDbModel
        {
            Word = model.Word
        };
    }

    public static WordModel ToModel(this WordDbModel model)
    {
        return new WordModel
        {
            Word = model.Word
        };
    }
}