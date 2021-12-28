import { Usuario } from './../models/Usuario';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UsuarioService } from './usuario.service';
import { NotificationService } from '../notification.service';

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
               private usuarioService: UsuarioService,
               private notifyService : NotificationService
               ) {

      this.criarForm();
   }

  ngOnInit(){
    this.isActive = true;
    this.carregarUsuarios();
  }

   formatDate()
  {
    this.usuarios.map( u => u.dataNascimento ? u.dataNascimento =
      ((new Date(u.dataNascimento).getDate()) +'/'+
      ((new Date(u.dataNascimento).getMonth() + 1)) +'/'+
        new Date(u.dataNascimento).getFullYear()):null)
  }

  formatStringDate(data: string)
  {
    let arrayData = data.split("/");
      if(arrayData[1].length===1){ arrayData[1]=`0${arrayData[1]}` }
      if(arrayData[0].length===1){ arrayData[0]=`0${arrayData[0]}` }

    return `${arrayData[2]}-${arrayData[1]}-${arrayData[0]}`
  }

  carregarUsuarios(){
     this.usuarioService.getAll().subscribe(
        (usuarios: Usuario[]) => {
          this.usuarios = usuarios;
          this.formatDate()
        },
        (erro: any) => {
          console.error(erro);
        }
      )
    }

    abrirCadastro(){
      this.usuarioSelecionado = new Usuario;
      this.usuarioSelecionado.sexoId = 1;
      this.cadastrar = true;
    }

    salvarUsuario(usuario: Usuario){

      if(!this.usuarioSelecionado.usuarioId){
        this.usuarioService.post(usuario).subscribe(
          (usuario) => {
            this.voltar();
            this.carregarUsuarios();
            this.notifyService.showSuccess("Cadastro realizado com sucesso!", "")
          },
          (error: any) => {
            console.log(error)
          });
      }else{
        usuario.usuarioId = this.usuarioSelecionado.usuarioId;
        this.usuarioService.put(this.usuarioSelecionado.usuarioId, usuario).subscribe(
          (usuario) => {
            this.carregarUsuarios();
          },
          (error: any) => {
            console.log(error);
          }
        );
      }
    }

    inativarAtivarUsuario(usuario: Usuario){

      let mensagem = "Usuário inativado com sucesso!";
      if(usuario.ativo===false){
        mensagem = "Usuário ativado com sucesso!"
      }

      usuario.dataNascimento = this.formatStringDate(usuario.dataNascimento);
      this.usuarioService.inativarAtivar(usuario.usuarioId, usuario).subscribe(
        (usuario) => {
          this.carregarUsuarios();
          this.notifyService.showSuccess(mensagem , "");

        },
        (error: any) => {
          console.log(error);
        }
      );
    }

    deletarUsuario(usuario: Usuario){
      this.modalRef?.hide();

      this.usuarioService.delete(usuario.usuarioId).subscribe(
        (usuario) => {
          this.usuarioSelecionado = null;
          this.cadastrar = false;
          this.carregarUsuarios();
          this.notifyService.showSuccess("Usuário excluído com sucesso!" , "");

        },
        (error: any) => {
          console.log(error);
        });
    }

    buscarPorNome(){
      let usuario = new Usuario();

       usuario.nome = this.text;
       usuario.ativo = this.isActive;

       this.usuarioService.getByName(usuario).subscribe(
          ( usuarios: Usuario[]) => {
            this.usuarios = usuarios;
            this.formatDate()
          },
          (error: any)=> {
            console.log(error);
          }
       )

       this.cadastrar = false;
   }

  criarForm(){

      this.usuarioForm = this.fb.group({
          nome: ['', Validators.required],
          email: ['', Validators.required],
          senha: ['', Validators.required],
          ativo: [1, Validators.required],
          dataNascimento: ['', Validators.required],
          sexoId: [1, Validators.required]

      });
  }


  usuarioSelect(usuario: Usuario){
    usuario.dataNascimento = this.formatStringDate(usuario.dataNascimento);
    this.usuarioSelecionado = usuario;
    this.usuarioForm.patchValue(usuario);

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
    this.carregarUsuarios();
    this.usuarioSelecionado = null;
    this.cadastrar = false;
  }

}
