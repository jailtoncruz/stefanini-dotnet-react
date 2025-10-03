import { TextField } from "@radix-ui/themes";
import { Form } from "radix-ui";

type InputType =
  | "number"
  | "search"
  | "time"
  | "text"
  | "hidden"
  | "tel"
  | "url"
  | "email"
  | "date"
  | "datetime-local"
  | "month"
  | "password"
  | "week"
  | undefined;

interface InputFieldProps {
  value?: string | number | undefined;
  onChange: (event: {
    name: string;
    value: string | number | Date | undefined;
  }) => void;
  type: InputType;
  name: string;
  placeholder?: string;
  label?: string;
  required?: boolean;
  className?: string;
}

export function InputField({
  value,
  onChange,
  type,
  name,
  placeholder,
  label,
  required,
  className,
}: InputFieldProps) {
  return (
    <Form.Field name={name} className={`flex flex-col ${className}`}>
      {label && (
        <Form.Label className="text-sm font-medium mb-1">{label}</Form.Label>
      )}

      <Form.Control asChild>
        <TextField.Root
          placeholder={placeholder}
          type={type}
          name={name}
          value={value}
          onChange={(e) =>
            onChange({ name: e.target.name, value: e.target.value })
          }
          required={required}
          className="p-2 border border-gray-500 rounded focus:outline-none invalid:bg-red-500"
        />
      </Form.Control>

      {required ? (
        <Form.Message match="valueMissing" className="text-xs">
          Campo obrigatório
        </Form.Message>
      ) : undefined}
      <Form.Message match="typeMismatch" className="text-xs">
        Valor inválido
      </Form.Message>
    </Form.Field>
  );
}
