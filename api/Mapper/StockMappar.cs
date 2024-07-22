using api.DTOs.Stock;
using api.Model;

namespace api.Mapper;

public static class StockMappar
{
    public static StockDto ToStockDto(this Stock stock){
        return new StockDto{

            Id = stock.Id,
            sympol = stock.sympol,
            Industry = stock.Industry,
            CompanyName = stock.CompanyName,
            MarketCap = stock.MarketCap,
            Purchase = stock.Purchase,
            Lastdev = stock.Lastdev,
            Comments = stock.Comments.Select(s => s.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromCreateDTO(this CreatStockDto stockDto)
    {
        return new Stock
        {
            sympol = stockDto.sympol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            Lastdev = stockDto.Lastdev,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
        public static Stock fromUpdateToStockDto(this UpdateStockDto UpdatestockDto)
        {
            return new Stock
            {
                sympol = UpdatestockDto.sympol,
                CompanyName = UpdatestockDto.CompanyName,
                Purchase = UpdatestockDto.Purchase,
                Lastdev = UpdatestockDto.Lastdev,
                Industry = UpdatestockDto.Industry,
                MarketCap = UpdatestockDto.MarketCap
            };
        }

}
