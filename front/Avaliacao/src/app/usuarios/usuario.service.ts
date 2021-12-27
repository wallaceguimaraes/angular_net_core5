import { Usuario } from './../models/Usuario';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

constructor(private http: HttpClient) { }

  baseUrl = `${environment.baseUrl}/usuario`;

  getAll(): Observable<Usuario[]>{
    return this.http.get<Usuario[]>(`${this.baseUrl}`);
  }

  getById(id: number): Observable<Usuario>{
    return this.http.get<Usuario>(`${this.baseUrl}/${id}`)
  }

  getByName(usuario: Usuario): Observable<Usuario[]>{
    return this.http.post<Usuario[]>(`${this.baseUrl}/GetByName`, usuario)
  }

  post(usuario: Usuario){
    return this.http.post(`${this.baseUrl}`, usuario);
  }

  put(id: Number, usuario: Usuario){
    return this.http.put(`${this.baseUrl}/${id}`, usuario);
  }

  inativarAtivar(id: Number, usuario: Usuario){
    return this.http.put(`${this.baseUrl}/InativarAtivarUsuario/${id}`, usuario);
  }

}
