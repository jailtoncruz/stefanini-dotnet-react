export const dateMask = (value: string) => {
  return value
    .replace(/\D/g, "") // Remove non-digits
    .replace(/(\d{2})(\d)/, "$1/$2") // DD/
    .replace(/(\d{2})\/(\d{2})(\d)/, "$1/$2/$3") // DD/MM/
    .replace(/(\d{2})\/(\d{2})\/(\d{4})\d+?$/, "$1/$2/$3"); // DD/MM/YYYY (limit year to 4 digits)
};
