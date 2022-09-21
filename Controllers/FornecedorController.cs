using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedorController : MainController

    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository fornecedorRepository;
        private readonly IFornecedorService fornecedorService;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorController(IFornecedorRepository fornecedorRepository, IMapper mapper, IFornecedorService fornecedorService, IEnderecoRepository enderecoRepository)
        {
            this.fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            this.fornecedorService = fornecedorService;
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(await fornecedorRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterpPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }
        

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            
            var result = await fornecedorService.Adicionar(fornecedor);
            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            var result = await fornecedorService.Atualizar(fornecedor);
            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir (Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            
            if(fornecedor == null) return BadRequest(); 

            await fornecedorService.Remover(id);

            var result = await fornecedorService.Remover(id);
            if (!result) return BadRequest();

            return Ok(fornecedor);

        }

        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        public async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await fornecedorRepository.ObterFornecedorEndereco(id));
        }


    }
}
