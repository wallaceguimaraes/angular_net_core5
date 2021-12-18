import { Usuario } from './../models/Usuario';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UsuarioService } from './usuario.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  public usuarioForm: FormGroup
  public titulo = 'Relatório dos Usuários';
  modalRef?: BsModalRef;
  public usuarioSelecionado: Usuario;

  public text: string;
  public isActive: boolean;
  public cadastrar = false;


  public usuarios: Usuario [];


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  constructor( private fb: FormBuilder,
               private modalService: BsModalService,
               private usuarioService: UsuarioService ) {
      this.criarForm();
   }

  ngOnInit(){
    this.carregarUsuarios();
  }

  carregarUsuarios(){
     this.usuarioService.getAll().subscribe(
        (usuarios: Usuario[]) => {
          this.usuarios = usuarios;
          this.usuarios.map( u => u.dataNascimento ? u.dataNascimento =
            ((new Date(u.dataNascimento).getDate()) +'/'+
            ((new Date(u.dataNascimento).getMonth() + 1)) +'/'+
              new Date(u.dataNascimento).getFullYear()):null)
        },
        (erro: any) => {
          console.error(erro);
        }
      )
    }


    abrirCadastro(){
      this.usuarioSelecionado = new Usuario;

      console.log(this.usuarioSelecionado.usuarioId)
       this.usuarioSelecionado.sexoId = 1;
        this.cadastrar = true;
    }

    salvarUsuario(usuario: Usuario){


      if(this.usuarioSelecionado.usuarioId==undefined){
        console.log("net")
      }



      this.usuarioService.put(usuario.usuarioId, usuario).subscribe(
        (usuario) => {
          this.carregarUsuarios();
          console.log(usuario)
        },
        (error: any) => {
          console.log(error);
        }
      )

    }

    buscarPorNome(){
      let usuario = new Usuario();

       usuario.nome = this.text;
       usuario.ativo = this.isActive;

       this.usuarioService.getByName(usuario).subscribe(
          ( usuarios: Usuario[]) => {
            this.usuarios = usuarios;
            console.log(usuarios)
          },
          (error: any)=> {
            console.log(error);
          }
       )


       this.cadastrar = false;


   }

  criarForm(){

      this.usuarioForm = this.fb.group({
          usuarioId: [''],
          nome: ['', Validators.required],
          email: ['', Validators.required],
          senha: ['', Validators.required],
          ativo: [1, Validators.required],
          nascimento: ['', Validators.required],
          sexo: [1, Validators.required]

      });
  }


  usuarioSelect(usuario: Usuario){
    this.usuarioSelecionado = usuario;
    this.usuarioForm.patchValue(usuario);

    console.log(this.usuarioSelecionado.usuarioId)
  }

  sexoSelect(sexo: Number){
    this.usuarioForm.setValue({sexo: sexo});
  }

  ativoSelect(isAtivo: boolean){
    this.usuarioForm.setValue({ativo: isAtivo});

  }


  usuarioSubmit(){
    this.salvarUsuario(this.usuarioForm.value);
  }

  voltar(){
    this.usuarioSelecionado = null;
    this.cadastrar = false;
  }




}