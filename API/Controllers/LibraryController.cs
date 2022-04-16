using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LibraryController : BaseApiController
    {
         private readonly ILibraryLedgerRepository _libraryLedgerRepository;
        private readonly IMapper _mapper;

        public LibraryController(ILibraryLedgerRepository libraryLedgerRepository, IMapper mapper)
        {
            _libraryLedgerRepository = libraryLedgerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryLedgerDto>>> GetLibraryLedgers(){
            var libraryLedgers = await _libraryLedgerRepository.GetLibraryLedgersAsync();

            var libraryLedgersToReturn = _mapper.Map<IEnumerable<LibraryLedgerDto>>(libraryLedgers);
            //ne dela brez ok
            return Ok(libraryLedgersToReturn);
        }

        [HttpGet("user/{id}")]
        public async Task< ActionResult<LibraryLedgerDto>> GeLibraryLedgersByUserIdAsync(int userId ){
            var user = await _libraryLedgerRepository.GeLibraryLedgersByUserIdAsync(userId);

            return _mapper.Map<LibraryLedgerDto>(user);
        }

        
        [HttpGet("user/{id}")]
        public async Task< ActionResult<LibraryLedgerDto>> GetLibraryLedgersByUserIdAsync(int userId ){
            var user = await _libraryLedgerRepository.GeLibraryLedgersByUserIdAsync(userId);

            return _mapper.Map<LibraryLedgerDto>(user);
        }
    }
}