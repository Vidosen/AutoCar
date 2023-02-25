// ICVR CONFIDENTIAL
// __________________
// 
// [2016] - [2023] ICVR LLC
// All Rights Reserved.
// 
// NOTICE:  All information contained herein is, and remains
// the property of ICVR LLC and its suppliers,
// if any.  The intellectual and technical concepts contained
// herein are proprietary to ICVR LLC
// and its suppliers and may be covered by U.S. and Foreign Patents,
// patents in process, and are protected by trade secret or copyright law.
// Dissemination of this information or reproduction of this material
// is strictly forbidden unless prior written permission is obtained
// from ICVR LLC.

using AutoCar.Models;
using AutoCar.Storage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoCar.Pages;

public class ParkingSeats : PageModel
{
    public IEnumerable<ParkingSeat> Seats { get; private set; } = Enumerable.Empty<ParkingSeat>();
    public void OnGet()
    {
        using (var storage = new PostgresStorage())
        {
            Seats = storage.ParkingSeats.Include(seat => seat.Contract).ToArray();
        }
    }
}