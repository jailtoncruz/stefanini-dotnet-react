import z from "zod";

export const cpfSchema = z
  .string()
  // .min(11, "CPF deve ter 11 digítos")
  // .max(14, "CPF deve ter 11 digítos")
  .refine((cpf) => {
    const cleanCPF = cpf.replace(/\D/g, "");

    if (cleanCPF.length !== 11 || /^(\d)\1+$/.test(cleanCPF)) {
      return false;
    }

    // CPF validation logic here (same as above)
    let sum = 0;
    for (let i = 0; i < 9; i++) {
      sum += parseInt(cleanCPF.charAt(i)) * (10 - i);
    }
    let remainder = (sum * 10) % 11;
    if (remainder === 10 || remainder === 11) remainder = 0;
    if (remainder !== parseInt(cleanCPF.charAt(9))) return false;

    sum = 0;
    for (let i = 0; i < 10; i++) {
      sum += parseInt(cleanCPF.charAt(i)) * (11 - i);
    }
    remainder = (sum * 10) % 11;
    if (remainder === 10 || remainder === 11) remainder = 0;
    if (remainder !== parseInt(cleanCPF.charAt(10))) return false;

    return true;
  }, "CPF Inválido");
