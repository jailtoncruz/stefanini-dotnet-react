import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { listPeople, createPerson, updatePerson } from "../services/api/person";

export const usePersonQuery = () => {
  return useQuery({
    queryKey: ["person"],
    queryFn: listPeople,
  });
};

export const useCreatePersonMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createPerson,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["person"] });
    },
  });
};

export const useUpdatePersonMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updatePerson,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["person"] });
    },
  });
};
