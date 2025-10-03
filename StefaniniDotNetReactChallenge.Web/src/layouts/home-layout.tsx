import { ExternalLink } from "lucide-react";
import type { ReactNode } from "react";

export function HomeLayout({ children }: { children: ReactNode }) {
  return (
    <div className="min-h-dvh flex flex-col bg-zinc-950">
      <header
        className="m-4 py-4 flex max-w-[1200px] flex-row mx-auto justify-between items-center
        w-full border-b-zinc-50 border-b"
      >
        <h1 className="text-gray-100 font-medium text-3xl">
          Cadastro de Pessoas
        </h1>

        <div className="flex flex-col items-end">
          <h2 className="text-gray-100 font-medium text-lg">
            Desafio Stefanini - .NET + React
          </h2>

          <div className="flex flex-row gap-2">
            <a
              href="/api/swagger"
              target="_blank"
              rel="noopener noreferrer"
              className="inline-flex items-center text-sky-300 hover:text-sky-500 transition-colors duration-200"
            >
              <span>Swagger</span>
              <ExternalLink size={16} className="ml-1" />
            </a>

            <a
              href="https://github.com/jailtoncruz/stefanini-dotnet-react"
              target="_blank"
              rel="noopener noreferrer"
              className="inline-flex items-center text-sky-300 hover:text-sky-500 transition-colors duration-200"
            >
              <span>Source Code</span>
              <ExternalLink size={16} className="ml-1" />
            </a>
          </div>
        </div>
      </header>

      <main className="flex-1 flex flex-col max-w-[1200px] mx-auto w-full overflow-hidden">
        {children}
      </main>
    </div>
  );
}
