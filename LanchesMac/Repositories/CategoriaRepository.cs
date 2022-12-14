using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories {

    public class CategoriaRepository : ICategoriaRepository {

        private readonly AppDBContext _context;

        public CategoriaRepository(AppDBContext context) {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
