using System.Threading.Tasks;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;

namespace EspacoPotencial.master.Areas.Cadastro.Models.Estoque
{
    public interface StateItem
    {
        Task<bool> ValidateItemAsync(item item, ApaDbContext context);
    }

    public class ValidItemState : StateItem
    {
        public async Task<bool> ValidateItemAsync(item item, ApaDbContext context)
        {
            var armazem = await context.armazem.FindAsync(item.estoque_id);
            if (armazem != null && armazem.tipo_armazem != item.tipo_armazem)
            {
                return false;
            }
            return true;
        }
    }

    public class InvalidItemState : StateItem
    {
        public async Task<bool> ValidateItemAsync(item item, ApaDbContext context)
        {
            return false;
        }
    }
}
