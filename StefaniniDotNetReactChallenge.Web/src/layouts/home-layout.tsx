import { Select } from "@radix-ui/themes";
import { ExternalLink } from "lucide-react";
import type { ReactNode } from "react";
import {
  useApiVersionMutation,
  useApiVersionQuery,
  type ApiVersion,
} from "../hooks/useApiVersion";

export function HomeLayout({ children }: { children: ReactNode }) {
  const { data } = useApiVersionQuery();
  const mutate = useApiVersionMutation();

  return (
    <div className="min-h-dvh flex flex-col bg-zinc-950">
      <header
        className="m-4 py-4 px-4 lg:px-0 flex max-w-[1200px] flex-row mx-auto justify-between items-center
        w-full border-b-zinc-50 border-b"
      >
        <h1 className="text-gray-100 font-medium text-lg md:text-2xl lg:text-3xl">
          Cadastro de Pessoas
        </h1>

        <div className="flex flex-col items-end gap-1">
          <h2 className="text-gray-100 font-medium text-sm md:text-base lg:text-lg">
            Desafio Stefanini - .NET + React
          </h2>

          <div className="flex flex-row gap-2 text-xs lg:text-sm text-nowrap">
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

            <Select.Root
              defaultValue="v1"
              value={data}
              onValueChange={(e) => {
                mutate.mutate(e as ApiVersion);
              }}
              size="1"
            >
              <Select.Trigger />
              <Select.Content>
                <Select.Item value="v1">API v1</Select.Item>
                <Select.Item value="v2">API v2</Select.Item>
              </Select.Content>
            </Select.Root>
          </div>
        </div>
      </header>

      <main className="flex-1 flex flex-col max-w-[1200px] mx-auto w-full overflow-hidden px-4 lg:px-0">
        {children}
      </main>
    </div>
  );
}
