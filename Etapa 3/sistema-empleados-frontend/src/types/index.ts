export interface Empleado {
  id: number;
  nombre: string;
  departamento: string;
  salario: number;
  fechaIngreso: string;
  cantidadVentas: number;
  totalVentas: number;
}

export interface CrearEmpleadoDto {
  nombre: string;
  departamento: string;
  salario: number;
}

export interface ActualizarSalarioDto {
  nuevoSalario: number;
}

export interface LoginDto {
  usuario: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiracion: string;
  usuario: string;
}

export interface ApiError {
  status: number;
  mensaje: string;
  fecha: string;
}
