import {
  useMutation,
  useQueryClient,
  useSuspenseQuery,
} from "@tanstack/react-query";

const apiVersionKey = "apiVersion";

export type ApiVersion = "v1" | "v2";

export const getApiVersion = () =>
  (localStorage.getItem(apiVersionKey) as ApiVersion) ?? "v1";

export const useApiVersionQuery = () => {
  return useSuspenseQuery({
    queryKey: [apiVersionKey],
    queryFn: getApiVersion,
  });
};

export const useApiVersionMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (version: ApiVersion) => {
      localStorage.setItem(apiVersionKey, version);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [apiVersionKey] });
    },
  });
};
