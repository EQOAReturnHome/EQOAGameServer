Message sent from client to server, this opcode is used after server select, same structure as [0x0904 Username & Password](Client/0x09XX/User&Pass.md)

Holds 2 unknown values, the string EQOA, username and password encrypted with AES128
Password is only held in the first 16 bytes of the Password row, remaining bytes were encrypted using 0x00'same

| Opcode                | 0x0001                                                             |
| unknown               | 0x00000003                                                         |
| unknown               | 0x00000004                                                         |
| EQOA                  | 0x45514f41                                                         |
| Name Length           | 0x0000000E                                                         |
| Black_Disaster (Name) | 0x426c61636b5f4469736173746572                                     |
| unknown               | 0x01                                                               |
| Password              | 0x6b4c2dac82ca28fd5208809949984553b71c12ae76c498fdf3ceeb444a0a49b5 |
