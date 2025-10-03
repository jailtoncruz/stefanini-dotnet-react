import { Select } from "@radix-ui/themes";
import { Form } from "radix-ui";

interface SelectItem {
  value: string;
  description: string;
  disabled?: boolean;
}

interface SelectFieldProps {
  items: SelectItem[];
  value: string;
  defaultValue?: string;
  onChange: (event: { name: string; value: string }) => void;
  name: string;
  label?: string;
  required?: boolean;
  className?: string;
}

export function SelectField({
  name,
  value,
  label,
  onChange,
  className,
  defaultValue,
  items,
}: SelectFieldProps) {
  return (
    <Form.Field name="gender" className={`flex flex-col ${className}`}>
      {label && (
        <Form.Label className="text-sm font-medium mb-1">{label}</Form.Label>
      )}
      <Form.Control asChild>
        <Select.Root
          defaultValue={defaultValue}
          onValueChange={(v) => onChange({ value: v, name })}
          value={value}
        >
          <Select.Trigger />
          <Select.Content>
            {items.map((item, index) => (
              <Select.Item
                key={index}
                value={item.value}
                disabled={item.disabled}
              >
                {item.description}
              </Select.Item>
            ))}
          </Select.Content>
        </Select.Root>
      </Form.Control>
    </Form.Field>
  );
}
