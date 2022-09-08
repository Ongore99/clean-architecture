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
    private ITransactionRepository? _transactionRepository;
    private bool disposed = false;


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
    
    public ITransactionRepository TransactionRepository
    {
        get
        {
            _transactionRepository ??= new TransactionRepository(_context, _localizer);
            return _transactionRepository;
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
    
    public async Task BeginTransactionAsync()  
    {  
        await _context.Database.BeginTransactionAsync();  
    } 
    
    public async Task CommitAsync(bool save = false)  
    {
        if (save)
        {
            await _context.SaveChangesAsync();
        }
        
        await _context.Database.CommitTransactionAsync();  
    } 
    
    public async Task RollbackAsync()  
    {  
        await _context.Database.RollbackTransactionAsync();  
    }  
}