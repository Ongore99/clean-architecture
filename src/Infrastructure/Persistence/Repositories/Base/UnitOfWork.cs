using System.Transactions;
using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IStringLocalizer<SharedResource> _localizer;
    
    private IAccountRepository? _accountRepository;
    private BaseRepository<Transaction>? transactionRepository;

    public UnitOfWork(AppDbContext context, IStringLocalizer<SharedResource> localizer)
    {
        _context = context;
        _localizer = localizer;
    }
        
    public IAccountRepository AccountRepository
    {
        get
        {
            _accountRepository ??= new AccountRepository(_context, _localizer);
            return _accountRepository;
        }
    }
    
    public IBaseRepository<Transaction> TransactionRepository
    {
        get
        {
            transactionRepository ??= new BaseRepository<Transaction>(_context, _localizer);
            return transactionRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}