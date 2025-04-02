using NaturguidenServerPrototype.Models;
using NaturguidenServerPrototype.Data;
using Microsoft.EntityFrameworkCore;

namespace NaturguidenServerPrototype.Services;

public class NatureSpotsService {
    private readonly ApplicationDbContext _context;

    public NatureSpotsService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<NatureSpot>> GetAllAsync() {
        return await _context.NatureSpots.ToListAsync();
    }

    public async Task<NatureSpot?> GetAsync(int id) {
        return await _context.NatureSpots.FindAsync(id);
    }

    public async Task<bool> HasUserVisitedAsync(int userId, int placeId) {
        return await _context.PlaceVisits
            .AnyAsync(pv => pv.UserId == userId && pv.PlaceId == placeId);
    }

    public async Task AddVisitAsync(int userId, int placeId) {
        var visit = new PlaceVisit {
            UserId = userId,
            PlaceId = placeId,
        };

        _context.PlaceVisits.Add(visit);
        await _context.SaveChangesAsync();
    }

}