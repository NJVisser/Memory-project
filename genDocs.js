var doxygen = require('doxygen');

var userOptions = {
  PROJECT_NAME: '"Memory Project"',
  SHOW_FILES: "YES",
  OPTIMIZE_OUTPUT_JAVA: "YES",
  EXTRACT_ALL: "YES",
  EXTRACT_PRIVATE: "YES",
  EXTRACT_PACKAGE: "YES",
  EXTRACT_STATIC: "YES",
  EXTRACT_LOCAL_CLASSES: "YES",
  EXTRACT_LOCAL_METHODS: "YES",
  SOURCE_BROWSER: "YES",
  OUTPUT_DIRECTORY: "Docs",
  INPUT: "./MemoryProject",
  EXCLUDE_PATTERNS: ["*/obj/*"],
  RECURSIVE: "YES",
  FILE_PATTERNS: ["*.cs", "*.md"],
  //EXTENSION_MAPPING: "js=Javascript",
  GENERATE_LATEX: "NO",
  USE_MDFILE_AS_MAINPAGE: "./README.md",
  HTML_DYNAMIC_SECTIONS: "YES",
  HTML_COLORSTYLE_HUE: 29,
  HTML_COLORSTYLE_SAT: 255,
  HTML_COLORSTYLE_GAMMA: 240
};


doxygen.downloadVersion().then(function(data) {
  doxygen.createConfig(userOptions);
  doxygen.run();
});
