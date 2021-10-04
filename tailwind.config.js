const colors = require('tailwindcss/colors')
module.exports = {
  purge: {
    enabled: true,
    content: [
      './**/*.html',
      './**/*.razor'
    ],
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    colors: {
      transparent: 'transparent',
      red: {
        light: "#af2f2f26"
      },
      gray: colors.trueGray,
      white: colors.white,
      blue: colors.blue,
      black: colors.black,
      placeholder: "#e6e6e6",
      deleteRed: "#cc9a9a",
      itemBorder: "#ededed",
      label: "#4d4d4d",
      footer: "#777",
      filterSelected: "#af2f2f33",
      filterHover: "#af2f2f1a",
      completed: "#d9d9d9",
      allChecked: "#737373"
    },
    fontFamily: {
      'sans': ['Helvetica Neue', 'Helvetica', 'Arial', 'sans-serif']
    },
    extend: {
      lineHeight: {
        'default': '1.4em'
      },
      minWidth: {
        '230px': '230px'
      },
      maxWidth: {
        '550px': '550px'
      },
      fontSize: {
        '100px': '100px',
        '14px': '14px',
        '24px': '24px',
        '22px': '22px',
        '30px': '30px'
      },
      padding: {
        '3px': '3px',
        '7px': '7px',
        '10px': '10px',
        '15px': '15px',
        '16px': '16px',
        '27px': '27px',
        '60px': '60px'
      },
      width: {
        '60px': '60px',
        '68px': '68px',
        '40px': '40px'
      },
      height: {
        '20px': '20px',
        '40px': '40px',
        '60px': '60px',
        '68px': '68px'
      },
      inset: {
        '24px': '24px',
        '25px': '25px'
      },
      backgroundImage: {
        'checked': 'url("/images/checked.svg")',
        'unchecked': 'url("/images/unchecked.svg")'
      },
      margin: {
        '3px': '3px',
        '5px': '5px'
      },
      boxShadow: {
        'headerInput': '0 -2px 1px rgb(0 0 0 / 3%) inset',
        'footer': '0 1px 1px rgb(0 0 0 / 20%), 0 8px 0 -3px #f6f6f6, 0 9px 1px -3px rgb(0 0 0 / 20%), 0 16px 0 -6px #f6f6f6, 0 17px 2px -6px rgb(0 0 0 / 20%)',
        'edit': '0 -1px 5px 0 rgb(0 0 0 / 20%) inset'
      },
      borderRadius: {
        '3px': '3px'
      }
    },
  },
  variants: {
    extend: {
      borderStyle: ['focus-visible', 'focus'],
      backgroundColor: ['checked'],
      borderColor: ['checked']
    },
  },
  plugins: [],
}
