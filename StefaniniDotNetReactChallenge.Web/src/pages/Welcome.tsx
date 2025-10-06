import { Button, Separator, TextField } from "@radix-ui/themes";
import { useState } from "react";
import { ArrowRightIcon } from "lucide-react";
import { authenticate } from "../services/api/auth/login";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export function Welcome() {
  const [name, setName] = useState<string>("");
  const router = useNavigate();

  const handleSubmit = async () => {
    try {
      const { token } = await authenticate(name);
      localStorage.setItem("authToken", token);
      router("/");
      toast(`Bem-vindo(a)!`);
    } catch (_err) {
      toast("Ops... Algo deu erro, tente novamente.");
      setName("");
    }
  };

  return (
    <div className="flex flex-col items-center justify-center flex-1">
      <div
        className="px-8 py-6 border border-gray-700 rounded
        max-w-xl bg-[#1a1a1a] flex flex-col gap-4"
      >
        <h1 className="font-bold text-4xl text-center">Bem-vindo!</h1>
        <p className="text-lg text-wrap text-justify">
          Essa aplicação tem como objetivo ser uma prova de conceito da
          implementação das tecnologias .NET no back-end e React no Front-end.
        </p>

        <Separator size="4" mb="4" />

        <form
          onSubmit={(e) => {
            e.preventDefault();
            e.stopPropagation();
            handleSubmit();
          }}
          className="flex flex-col gap-4"
        >
          <TextField.Root
            placeholder="Insira um dos nomes chaves (Jailton, Stefanini ou Andressa)."
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            size="3"
            variant="surface"
            color="gray"
            className="border border-gray-500 rounded focus:outline-none text-lg"
          />

          <Button
            className="flex-1 my-4 text-lg"
            size="3"
            disabled={name.length < 3}
            type="submit"
          >
            Acessar <ArrowRightIcon />
          </Button>
        </form>

        <span className="text-xs text-gray-300 text-center">
          *O nome inserido não será salvo, será utilizado apenas para
          autenticação.
        </span>
      </div>
    </div>
  );
}
