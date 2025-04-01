using infrastrucure.Models;
using service.Models;

namespace infrastrucure.Mappers;

public static class WordMapper
{
    public static WordDbModel ToDbModel(this WordModel model)
    {
        return new WordDbModel
        {
            Id = model.Id.Value,
            Word = model.Word
        };
    }

    public static WordModel ToModel(this WordDbModel model)
    {
        return new WordModel
        {
            Id = new WordId(model.Id),
            Word = model.Word
        };
    }
}