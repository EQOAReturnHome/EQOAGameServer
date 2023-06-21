local items = require("Scripts/items")
local quests = {
    --Magician(10) Human(0) Eastern(1)
    [100101] = {
        [0] = {log = "You must purchase a iron ring from Merchant Yulia, then return to Malsis."},
        [1] = {xp = 430}
    },
    [100102] = {
        [0] = {log = "Go speak to Spiritmaster Alshan."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Malsis."},
        [3] = {xp = 2200}
    },
    [100103] = {
        [0] = {log = "Return two cracked ant pincers to Kellina."},
        [1] = {
            xp = 6900,
            requirements = {{items.CRACKED_ANT_PINCER, 2}},
            rewards = {{items.SMOLDERING_AURA, 1}}
        }
    },
    [100104] = {
        [0] = {
            log = "Kellina needs a plain robe from Merchant Yulia, a silk cord from Merchant Yesam, and a ruined bat wing."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.PLAIN_ROBE, 1},
                {items.SILK_CORD, 1},
                {items.RUINED_BAT_WING, 1}
            },
            rewards = {{items.BLUE_ROBE, 1}}
        }
    },
    [100105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Kellina."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.MOTIVATE, 1}}
        }
    },
    [100107] = {
        [0] = {log = "Bring Kellina a Chichan Eel venom sac. You will find it north of Freeport, along the river."},
        [1] = {
            xp = 157474,
            requirements = {{items.EEL_VENOM_SAC, 1}},
            rewards = {
                {items.INFUSION, 1},
                {items.BLACKENED_LEGGINGS, 1}
            }
        }
    },
    [100110] = {
        [0] = {log = "Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport."},
        [1] = {
            log = " Return to Guard Sareken with 147 tunar and three tough pike scales.",
            requirements = {{items.TOUGH_PIKE_SCALE, 3}}
        },
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its club as proof."
        },
        [3] = {
            xp = 556753,
            requirements = {{items.ORC_RANSACKERS_CLUB, 1}},
            rewards = {{items.STURDY_STAFF, 1}}
        }
    },
    [100113] = {
        [0] = {
            log = "See Ilenar in the Shining Shield guild hall.",
            rewards = {{items.DAMAGED_ROBE, 1}}
        },
        [1] = {
            log = " Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. ",
            requirements = {{items.DAMAGED_ROBE, 1}}
        },
        [2] = {
            log = " Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger.",
            requirements = {{items.FINE_CHOCOLATES, 1}}
        },
        [3] = {
            log = "Return to Delwin Stitchfinger.",
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}}
        },
        [4] = {
            log = "Return the robe to Ilenar Crelwin.",
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [5] = {
            log = 'Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says "thanks".',
            requirements = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {
            xp = 550698,
            rewards = {{items.LAVA_WIND, 1}}
        }
    },
    [100115] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {
            log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof.",
            requirements = {{items.BLOODFIN_BROOD_MOTHER_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [2] = {
            log = "Return to Ilenar Crelwin.",
            requirements = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [3] = {
            log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood.",
            requirements = {{items.BLOOD_OF_THE_DESERT_MADMAN, 1}}
        },
        [4] = {log = "Return to Kellina."},
        [5] = {
            xp = 883791,
            rewards = {
                {items.ENDURE_FIRE, 1},
                {items.SUMMONERS_GARB, 1}
            }
        }
    },
    [100120] = {
        [0] = {log = "You must speak to Ilenar about your mission."},
        [1] = {
            log = "Go find Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
        },
        [2] = {log = "Go find Page Joseph Robert and offer to deliver his missives for him."},
        [3] = {
            log = "Bring the missives from Qeynos to Crim Arikson.",
            requirements = {{items.MISSIVES_FROM_QEYNOS, 1}},
            rewards = {{items.FORGED_LETTER, 1}}
        },
        [4] = {
            log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar.",
            requirements = {{items.AMULET_OF_DECEPTION, 1}}
        },
        [5] = {
            log = "Travel to the Temple of Light, west of Freeport. Present Leandro Novan with the forged letter.",
            requirements = {{items.FORGED_LETTER, 1}}
        },
        [6] = {
            log = "Speak to Praetor Gunner about Geldwins Grimoire.",
            rewards = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [7] = {
            log = "Bring the Grimoire to Ilenar.",
            requirements = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [8] = {log = "Return to Kellina for your reward."},
        [9] = {
            xp = 2814929,
            rewards = {
                {items.FROZEN_MARK, 1},
                {items.MAGICAL_STAFF, 1},
                {items.LAVA_STONE, 1},
                {items.MYSTICAL_TOME, 1}
            }
        }
    },
    ------
    --Bard(5) Human(0) Eastern(1)
    [50101] = {
        [0] = {log = "You must purchase a raw silk boots from Merchant Dolson, then return to William Corufost."},
        [1] = {xp = 430}
    },
    [50102] = {
        [0] = {log = "Go speak to Spiritmaster Imaryn."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to William Corufost."},
        [3] = {xp = 2200}
    },
    [50103] = {
        [0] = {log = "Return two damaged dragonfly wings to Solenia Freyar."},
        [1] = {
            xp = 6900,
            requirements = {{items.DAMAGED_DRAGONFLY_WING, 2}},
            rewards = {{items.CHANT_OF_BATTLE, 1}}
        }
    },
    [50104] = {
        [0] = {
            log = "Purchase an iron ore and a leather strip from Merchant Shohan. Then hunt for a beetle leg segment. Return to Solenia Freyar."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.IRON_ORE, 1},
                {items.LEATHER_STRIP, 1},
                {items.BEETLE_LEG_SEGMENT, 1}
            },
            rewards = {{items.TRAVELERS_RAPIER, 1}}
        }
    },
    [50105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Solenia Freyar."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.FUNERAL_MARCH, 1}}
        }
    },
    [50107] = {
        [0] = {
            log = "Bring Solenia Freyar a Chichan Eel venom sac. You will find it north of Freeport, along the river."
        },
        [1] = {
            xp = 157474,
            requirements = {{items.EEL_VENOM_SAC, 1}},
            rewards = {
                {items.ARTFUL_STRIKE, 1},
                {items.SILKEN_LEGGINGS, 1}
            }
        }
    },
    [50110] = {
        [0] = {
            log = "Guard Sareken is in need of assistance, please go and find him at the north entrance of Freeport."
        },
        [1] = {log = " Return to Guard Sareken with 147 tunar and three tough pike scales."},
        requirements = {{items.TOUGH_PIKE_SCALE, 3}},
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its boots as proof."
        },
        [3] = {
            xp = 556753,
            requirements = {{items.ORC_RANSACKERS_BOOTS, 1}},
            rewards = {{items.STURDY_SHORT_SWORD, 1}}
        }
    },
    [50113] = {
        [0] = {
            log = "See Ilenar in the Shining Shield guild hall.",
            rewards = {{items.DAMAGED_ROBE, 1}}
        },
        [1] = {
            log = "Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. ",
            requirements = {{items.DAMAGED_ROBE, 1}}
        },
        [2] = {
            log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger.",
            requirements = {{items.FINE_CHOCOLATES, 1}}
        },
        [3] = {
            log = "Return to Delwin Stitchfinger.",
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}}
        },
        [4] = {
            log = "Return the robe to Ilenar Crelwin.",
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [5] = {log = "Report to Solenia."},
        [6] = {
            log = 'Give the special chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says "thanks".',
            requirements = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [7] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [8] = {
            xp = 550698,
            rewards = {{items.ANTHEM_OF_LIGHT, 1}}
        }
    },
    [50115] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {
            log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof.",
            requirements = {{items.BLOODFIN_BROOD_MOTHER_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [2] = {
            log = "Return to Ilenar Crelwin.",
            requirements = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [3] = {
            log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood.",
            requirements = {{items.BLOOD_OF_THE_DESERT_MADMAN, 1}}
        },
        [4] = {log = "Return to Solenia."},
        [5] = {
            xp = 883791,
            rewards = {
                {items.CRASHING_VERSES, 1},
                {items.THESPIAN_LEGGINGS, 1}
            }
        }
    },
    [50120] = {
        [0] = {log = "You must speak to Ilenar about your mission."},
        [1] = {
            log = "Go find Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
        },
        [2] = {
            log = "Go find Page Joseph Robert and offer to deliver his missives for him.",
            rewards = {{items.MISSIVES_FROM_QEYNOS, 1}}
        },
        [3] = {
            log = "Bring the missives from Qeynos to Crim Arikson.",
            requirements = {{items.MISSIVES_FROM_QEYNOS, 1}},
            rewards = {{items.FORGED_LETTER, 1}}
        },
        [4] = {
            log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar.",
            requirements = {{items.AMULET_OF_DECEPTION, 1}}
        },
        [5] = {log = "Go speak with Solenia to prepare for your journey."},
        [6] = {
            log = "Exit Freeport from the north, then travel west to reach the Temple of Light. Present Leandro Novan with the forged letter.",
            requirements = {{items.FORGED_LETTER, 1}}
        },
        [7] = {
            log = "Speak to Praetor Gunner about Geldwins Grimoire.",
            rewards = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [8] = {
            log = "Bring the Grimoire to Solenia.",
            requirements = {{items.GELDWINS_GRIMOIRE, 1}},
            rewards = {{items.FAKE_GELDWINS_GRIMOIRE, 1}}
        },
        [9] = {
            log = "Bring the Fake Grimoire to Ilenar.",
            requirements = {{items.FAKE_GELDWINS_GRIMOIRE, 1}}
        },
        [10] = {log = "Return to Solenia for your reward."},
        [11] = {
            xp = 2814929,
            requirements = {{items.GELDWINS_GRIMOIRE, 1}},
            rewards = {
                {items.POWER_DANCE, 1},
                {items.MAGIC_FOIL, 1},
                {items.SWEEPING_FLURRY, 1},
                {items.MAGIC_SABRE, 1}
            }
        }
    },
    ------
    --Cleric(9) Human(0) Eastern(1)
    [90101] = {
        [0] = {log = "You must purchase a petitioner's cap from Merchant Olkan, then return to Denouncer Alshea."},
        [1] = {xp = 430}
    },
    [90102] = {
        [0] = {log = "Go speak to Spiritmaster Keika."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Denouncer Alshea."},
        [3] = {xp = 2200}
    },
    [90103] = {
        [0] = {log = "Return two tarantula legs to Sister Falhelm."},
        [1] = {
            xp = 6900,
            requirements = {{items.TARANTULA_LEG, 2}},
            rewards = {{items.MINOR_BLESSING, 1}}
        }
    },
    [90104] = {
        [0] = {
            log = "Purchase 1 iron ore and also 1 leather strip from Merchant Shohan. Then hunt for 1 ant leg segment. Return to Sister Falhelm."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.IRON_ORE, 1},
                {items.LEATHER_STRIP, 1},
                {items.ANT_LEG_SEGMENT, 1}
            },
            rewards = {{items.ACOLYTES_MACE, 1}}
        }
    },
    [90105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Sister Falhelm."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.HOLY_SHOCK, 1}}
        }
    },
    [90107] = {
        [0] = {
            log = "Bring Sister Falhelm a Chichan Eel venom sac. You will find it north of Freeport, along the river."
        },
        [1] = {
            xp = 157474,
            requirements = {{items.EEL_VENOM_SAC, 1}},
            rewards = {
                {items.ENDURE_AILMENT, 1},
                {items.TRUBARS_LEGGINGS, 1}
            }
        }
    },
    [90110] = {
        [0] = {log = "Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport."},
        [1] = {log = " Return to Guard Sareken with 147 tunar and three tough pike scales."},
        requirements = {{items.TOUGH_PIKE_SCALE, 3}},
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its pouch as proof."
        },
        [3] = {
            xp = 556753,
            requirements = {{items.ORC_RANSACKERS_POUCH, 1}},
            rewards = {{items.STURDY_MACE, 1}}
        }
    },
    [90113] = {
        [0] = {
            log = "See Ilenar in the Shining Shield guild hall.",
            rewards = {{items.DAMAGED_ROBE, 1}}
        },
        [1] = {
            log = "Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. ",
            requirements = {{items.DAMAGED_ROBE, 1}}
        },
        [2] = {
            log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger.",
            requirements = {{items.FINE_CHOCOLATES, 1}}
        },
        [3] = {
            log = "Return to Delwin Stitchfinger.",
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}}
        },
        [4] = {
            log = "Return the robe to Ilenar Crelwin.",
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [5] = {
            log = 'Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says "thanks".',
            requirements = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {
            xp = 550698,
            rewards = {{items.WARD_DEATH, 1}}
        }
    },
    [90115] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {
            log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof.",
            requirements = {{items.BLOODFIN_BROOD_MOTHER_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [2] = {
            log = "Return to Ilenar Crelwin.",
            requirements = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [3] = {
            log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood.",
            requirements = {{items.BLOOD_OF_THE_DESERT_MADMAN, 1}}
        },
        [4] = {log = "Return to Sister Falhelm."},
        [5] = {
            xp = 883791,
            rewards = {
                {items.ENDURE_AFFLICTION, 1},
                {items.REVERENT_BRACERS, 1}
            }
        }
    },
    [90120] = {
        [0] = {log = "You must speak to Ilenar about your mission."},
        [1] = {
            log = "Go speak with Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
        },
        [2] = {log = "Go find Page Joseph Robert and offer to deliver his missives for him."},
        [3] = {
            log = "Bring the missives from Qeynos to Crim Arikson.",
            requirements = {{items.MISSIVES_FROM_QEYNOS, 1}},
            rewards = {{items.FORGED_LETTER, 1}}
        },
        [4] = {
            log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar.",
            requirements = {{items.AMULET_OF_DECEPTION, 1}}
        },
        [5] = {
            log = "Travel to the Temple of Light, west of Freeport. Present Leandro Novan with the forged letter.",
            requirements = {{items.FORGED_LETTER, 1}}
        },
        [6] = {
            log = "Speak to Praetor Gunner about Geldwins Grimoire.",
            rewards = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [7] = {
            log = "Bring the Grimoire to Ilenar.",
            requirements = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [8] = {log = "Return to Sister for your reward."},
        [9] = {
            xp = 2814929,
            rewards = {
                {items.DISEASE_WARD, 1},
                {items.MAGICAL_STAFF, 1},
                {items.FIELD_DRESS, 1},
                {items.ENCHANTED_MACE, 1}
            }
        }
    },
    ------
    --Rogue(6) Human(0) Eastern(1)
    [60101] = {
        [0] = {log = "You must purchase a burglar's gloves from Merchant Olkan, then return to Necorik the Ghost."},
        [1] = {xp = 430}
    },
    [60102] = {
        [0] = {log = "Go speak to Spiritmaster Keika."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Necorik the Ghost."},
        [3] = {xp = 2200}
    },
    [60103] = {
        [0] = {log = "Return two ruined snake scales to Athera."},
        [1] = {
            xp = 6900,
            requirements = {{items.RUINED_SNAKE_SCALE, 2}},
            rewards = {{items.SNEAK, 1}}
        }
    },
    [60104] = {
        [0] = {
            log = "Purchase 1 iron ore from Merchant Shohan, and 1 ivory from Merchant Yesam. Hunt for 1 beetle leg segment. Return to Athera."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.IRON_ORE, 1},
                {items.IVORY, 1},
                {items.BEETLE_LEG_SEGMENT, 1}
            },
            rewards = {{items.TIGER_DIRK, 1}}
        }
    },
    [60105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Athera."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.QUICK_BLADE, 1}}
        }
    },
    [60107] = {
        [0] = {log = "Bring Athera a Chichan Eel venom sac. You will find it north of Freeport, along the river."},
        [1] = {
            xp = 157474,
            requirements = {{items.EEL_VENOM_SAC, 1}},
            rewards = {
                {items.ACROBATICS, 1},
                {items.SHINING_PROTECTORS, 1}
            }
        }
    },
    [60110] = {
        [0] = {log = "Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport."},
        [1] = {log = " Return to Guard Sareken with 147 tunar and three tough pike scales."},
        requirements = {{items.TOUGH_PIKE_SCALE, 3}},
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its belt as proof."
        },
        [3] = {
            xp = 556753,
            requirements = {{items.ORC_RANSACKERS_BELT, 1}},
            rewards = {{items.STURDY_DAGGER, 1}}
        }
    },
    [60113] = {
        [0] = {
            log = "See Ilenar in the Shining Shield guild hall.",
            rewards = {{items.DAMAGED_ROBE, 1}},
            [1] = {
                log = "Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. "
            },
            requirements = {{items.DAMAGED_ROBE, 1}}
        },
        [2] = {
            log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger.",
            requirements = {{items.FINE_CHOCOLATES, 1}}
        },
        [3] = {
            log = "Return to Delwin Stitchfinger.",
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}}
        },
        [4] = {
            log = "Return the robe to Ilenar Crelwin.",
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [5] = {
            log = 'Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says "thanks".'
        },
        requirements = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}},
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {
            xp = 550698,
            rewards = {{items.NIGHT_BREATH, 1}}
        }
    },
    [60115] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {
            log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof.",
            requirements = {{items.BLOODFIN_BROOD_MOTHER_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [2] = {
            log = "Return to Ilenar Crelwin.",
            requirements = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [3] = {
            log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood.",
            requirements = {{items.BLOOD_OF_THE_DESERT_MADMAN, 1}}
        },
        [4] = {log = "Return to Athera."},
        [5] = {
            xp = 883791,
            rewards = {
                {items.VAULTERS_BALANCE, 1},
                {items.SHADOWPAD_BOOTS, 1}
            }
        }
    },
    [60120] = {
        [0] = {log = "You must speak to Ilenar about your mission."},
        [1] = {
            log = "Go speak with Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
        },
        [2] = {log = "Go find Page Joseph Robert and offer to deliver his missives for him."},
        [3] = {
            log = "Bring the missives from Qeynos to Crim Arikson.",
            requirements = {{items.MISSIVES_FROM_QEYNOS, 1}},
            rewards = {{items.FORGED_LETTER, 1}}
        },
        [4] = {
            log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar.",
            requirements = {{items.AMULET_OF_DECEPTION, 1}}
        },
        [5] = {
            log = "Travel to the Temple of Light, west of Freeport. Present Leandro Novan with the forged letter.",
            requirements = {{items.FORGED_LETTER, 1}}
        },
        [6] = {
            log = "Speak to Praetor Gunner about Geldwins Grimoire.",
            rewards = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [7] = {
            log = "Bring the Grimoire to Ilenar.",
            requirements = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [8] = {log = "Return to Athera for your reward."},
        [9] = {
            xp = 2814929,
            rewards = {
                {items.AVOIDANCE, 1},
                {items.MAGIC_DAGGER, 1},
                {items.MINOR_WOUND, 1},
                {items.MAGIC_RAPIER, 1}
            }
        }
    },
    ------
    --Warrior(0) Human(0) Eastern(1)
    [101] = {
        [0] = {log = "You must purchase a militia bracer from Merchant Galosh, then return to Commander Nothard."},
        [1] = {xp = 430}
    },
    [102] = {
        [0] = {log = "Go speak to Spiritmaster Zole."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Commander Nothard."},
        [3] = {xp = 2200}
    },
    [103] = {
        [0] = {log = "Return two slashed pawn belts to Captain Norgam."},
        [1] = {
            xp = 6900,
            requirements = {{items.SLASHED_PAWN_BELT, 2}},
            rewards = {{items.KICK, 1}}
        }
    },
    [104] = {
        [0] = {
            log = "Purchase 1 iron ore and 1 leather strip from Merchant Shohan. Hunt for 2 broken vulture feathers. Return to Captain Norgam."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.IRON_ORE, 1},
                {items.LEATHER_STRIP, 1},
                {items.BROKEN_VULTURE_FEATHER, 2}
            },
            rewards = {{items.MILITIA_SHORT_SWORD, 1}}
        }
    },
    [105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Captain Norgam."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.TAUNT, 1}}
        }
    },
    [107] = {
        [0] = {
            log = "Bring Captain Norgam a Chichan Eel venom sac. You will find it north of Freeport, along the river."
        },
        [1] = {
            xp = 157474,
            requirements = {{items.EEL_VENOM_SAC, 1}},
            rewards = {
                {items.FURIOUS_DEFENSE, 1},
                {items.DRUBENS_LEGGINGS, 1}
            }
        }
    },
    [110] = {
        [0] = {log = "Guard Sareken is in need of assistance. Go find him at the north entrance of Freeport."},
        [1] = {
            log = " Return to Guard Sareken with 147 tunar and three tough pike scales.",
            requirements = {{items.TOUGH_PIKE_SCALE, 3}}
        },
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its cap as proof."
        },
        [3] = {
            xp = 556753,
            requirements = {{items.ORC_RANSACKERS_CAP, 1}},
            rewards = {{items.STURDY_TWO_HANDED_SWORD, 1}}
        }
    },
    [113] = {
        [0] = {
            log = "See Ilenar in the Shining Shield guild hall.",
            rewards = {{items.DAMAGED_ROBE, 1}}
        },
        [1] = {
            log = "Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. ",
            requirements = {{items.DAMAGED_ROBE, 1}}
        },
        [2] = {
            log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger.",
            requirements = {{items.FINE_CHOCOLATES, 1}}
        },
        [3] = {
            log = "Return to Delwin Stitchfinger.",
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}}
        },
        [4] = {
            log = "Return the robe to Ilenar Crelwin.",
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [5] = {
            log = 'Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says "thanks".',
            requirements = {{items.SPECIAL_BOX_OF_CHOCOLATES, 1}}
        },
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {
            xp = 550698,
            rewards = {{items.RAPID_STRIKE, 1}}
        }
    },
    [115] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {
            log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof.",
            requirements = {{items.BLOODFIN_BROOD_MOTHER_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [2] = {
            log = "Return to Ilenar Crelwin.",
            requirements = {{items.NIGHTWORM_ROOTS, 1}}
        },
        [3] = {
            log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood.",
            requirements = {{items.BLOOD_OF_THE_DESERT_MADMAN, 1}}
        },
        [4] = {log = "Return to Captain Norgam."},
        [5] = {
            xp = 883791,
            rewards = {
                {items.STOMP, 1},
                {items.FIRESPLINT_LEGGINGS, 1}
            }
        }
    },
    [120] = {
        [0] = {log = "You must speak to Ilenar about your mission."},
        [1] = {
            log = "Go speak with Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
        },
        [2] = {log = "Go find Page Joseph Robert and offer to deliver his missives for him."},
        [3] = {
            log = "Bring the missives from Qeynos to Crim Arikson.",
            requirements = {{items.MISSIVES_FROM_QEYNOS, 1}},
            rewards = {{items.FORGED_LETTER, 1}}
        },
        [4] = {
            log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar.",
            requirements = {{items.AMULET_OF_DECEPTION, 1}}
        },
        [5] = {
            log = "Travel to the Temple of Light, west of Freeport. Present Leandro Novan with the forged letter.",
            requirements = {{items.FORGED_LETTER, 1}}
        },
        [6] = {
            log = "Speak to Praetor Gunner about Geldwins Grimoire.",
            rewards = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [7] = {
            log = "Bring the Grimoire to Ilenar.",
            requirements = {{items.GELDWINS_GRIMOIRE, 1}}
        },
        [8] = {log = "Return to Captain Norgam for your reward."},
        [9] = {
            xp = 2814929,
            rewards = {
                {items.BELLOW, 1},
                {items.ENSORCELLED_GREATAXE, 1},
                {items.PILLAR_OF_MIGHT, 1},
                {items.ENSORCELLED_LONGSWORD, 1}
            }
        }
    },
    ------
    --Enchanter(12) Human(0) Eastern(1)
    [120101] = {
        [0] = {log = "You must purchase a bronze ring from Merchant Yulia, then return to Azlynn."},
        [1] = {xp = 430}
    },
    [120102] = {
        [0] = {log = "Go speak to Spiritmaster Alshan."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Azlynn."},
        [3] = {xp = 2200}
    },
    [120103] = {
        [0] = {log = "Return two beetle carapace fragments to Opanheim."},
        [1] = {
            xp = 6900,
            requirements = {{items.BEETLE_CARAPACE_FRAGMENT, 2}},
            rewards = {{items.CRAWLING_SKIN, 1}}
        }
    },
    [120104] = {
        [0] = {
            log = "Purchase 1 plain robe and 1 silk cord from Merchant Shohan. Hunt for a ruined spider fur. Return to Opanheim."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.PLAIN_ROBE, 1},
                {items.SILK_CORD, 1},
                {items.RUINED_SPIDER_FUR, 2}
            },
            rewards = {{items.YELLOW_ENCHANTERS_ROBE, 1}}
        }
    },
    [120105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Opanheim."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.HEAVY_ARMS, 1}}
        }
    },
    [120107] = {
        [0] = {log = "Find Smuggler Bandelan in Temby to the north. Bring Azlynn the rune stone of Ghizsa."},
        [1] = {
            xp = 157474,
            requirements = {{items.RUNE_STONE_OF_GHIZSA, 1}},
            rewards = {
                {items.ENDURE_ARCANE, 1},
                {items.PAPYRUS_WRIST_WRAPS, 1}
            }
        }
    },
    [120110] = {
        [0] = {
            log = "Follow the riverbank north of Freeport to the broken bridge. At night Glarik may appear. Bring Azlynn the sparkling green gem."
        },
        requirements = {{items.SPARKLING_GREEN_GEM, 1}},
        [1] = {log = "Purchase lantern oil from Merchant  Landi and return to Azlynn."},
        [2] = {
            xp = 556753,
            requirements = {{items.LANTERN_OIL, 1}},
            rewards = {{items.ROD_OF_EYES, 1}}
        }
    },
    [120113] = {
        rewards = {{items.COINS, 1}},
        [0] = {
            log = "Go see  Agent Wilkenson. He is on the docks. ",
            requirements = {{items.COINS, 1}},
            rewards = {{items.NOTE_FROM_WILKINSON, 1}}
        },
        [1] = {
            log = "Go find Duminven. Look for him at Saerk's Tower.",
            requirements = {{items.NOTE_FROM_WILKINSON, 1}}
        },
        [2] = {
            log = "Head into Bastable village, locate the thief Eliene and follow her. Kill and loot both her and her contact. Return to Duminven.",
            requirements = {{items.CRACKED_RED_CRYSTAL, 1}}
        },
        [3] = {log = "Return to the Mark of Louhmanta to Azlynn."},
        [4] = {
            xp = 550698,
            rewards = {{items.LUMBERING_ARMS, 1}}
        }
    },
    [120115] = {
        [0] = {log = "Go find Tailor Weynia, she can be found near the lighthouse, south of Freeport."},
        [1] = {
            log = "Search for and slay sidewinder snakes. Collect a skin and return it to Tailor Weynia.",
            requirements = {{items.SIDEWINDER_SKIN, 1}}
        },
        [2] = {
            log = "Search for and slay sand skippers. Collect a carapace and return it to Tailor Weynia.",
            requirements = {{items.SAND_SKIPPER_CARAPACE, 1}}
        },
        [3] = {
            log = "Search for and slay Gargantula. Collect a bundle of pristine silk and return it to Tailor Weynia.",
            requirements = {{items.BUNDLE_OF_PRISTINE_SILK, 1}}
        },
        [4] = {
            log = "Go to the west gate of Freeport and purchase vulture feathers from Dteven Savis and return to Tailor Weynia.",
            requirements = {{items.VULTURE_FEATHERS, 1}},
            rewards = {{items.POACHERS_LEGGINGS, 1}}
        },
        [5] = {log = "Go speak with Azlynn."},
        [6] = {
            xp = 883791,
            requirements = {{items.POACHERS_LEGGINGS, 1}},
            rewards = {
                {items.LEGGINGS_OF_INSIGHT, 1},
                {items.ALARMING_VISAGE, 1}
            }
        }
    },
    [120120] = {
        [0] = {log = "Go speak to Agent Wilkenson."},
        [1] = {log = "Go speak to Telina the Dark Witch."},
        [2] = {
            log = "A troll has your mark. Search southwest for the nasehir camps. Take the mark from Roj Eir Sew' Eil and return to Telina.",
            requirements = {{items.MARK_OF_LOUOTH, 1}}
        },
        [3] = {
            log = "Off the coast of MTG, slay skeletons and search the chests for the Chiseled Great Axe of Doom. Return to Telina the Dark Witch.",
            requirements = {{items.CHISELED_GREAT_AXE_OF_DOOM, 1}}
        },
        [4] = {
            log = "Go to Razor Back Fang mountain, southwest of Freeport. Find a waterlogged chest near an obelisk. Bring Telina the treasure.",
            requirements = {{items.ETCHED_HELMET_OF_GREATNESS, 1}},
            rewards = {{items.NOTE_FROM_TELINA, 1}}
        },
        [5] = {log = "Go speak to Azlynn."},
        [6] = {
            xp = 2814929,
            rewards = {
                {items.SPACIOUS_MIND, 1},
                {items.MAGIC_SCEPTER, 1},
                {items.POWER_BOON, 1},
                {items.MAGIC_BOOK, 1}
            }
        }
    },
    ------
    --Necromancer(11) Human(0) Eastern(1)
    [110101] = {
        [0] = {log = "You must purchase a bone earring from Merchant Gilgash, then return to Corious Slaerin."},
        [1] = {xp = 430}
    },
    [110102] = {
        [0] = {log = "Go speak to Spiritmaster Keika."},
        [1] = {log = "Go speak to Coachman Ronks."},
        [2] = {log = "Go speak to Corious Slaerin."},
        [3] = {xp = 2200}
    },
    [110103] = {
        [0] = {log = "Return two damaged dragonfly wings to Rathei Slaerin."},
        [1] = {
            xp = 6900,
            requirements = {{items.DAMAGED_DRAGONFLY_WING, 2}},
            rewards = {{items.LIFE_TAP, 1}}
        }
    },
    [110104] = {
        [0] = {
            log = "Purchase 1 plain robe and 1 silk cord from Merchant Yesam. Hunt for a sliver of snake meat. Return to Rathei Slaerin."
        },
        [1] = {
            xp = 17000,
            requirements = {
                {items.PLAIN_ROBE, 1},
                {items.SILK_CORD, 1},
                {items.SLIVER_OF_SNAKE_MEAT, 1}
            },
            rewards = {{items.BLACK_ROBE, 1}}
        }
    },
    [110105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Rathei Slaerin."
        },
        [1] = {
            xp = 36500,
            requirements = {{items.STOLEN_GOODS, 1}},
            rewards = {{items.RABID_INFECTION, 1}}
        }
    },
    [110107] = {
        [0] = {log = "Find Janxt in Temby to the north. Bring Corious Slaerin the cracked symbol of Tunare."},
        [1] = {
            xp = 157474,
            requirements = {{items.CRACKED_SYMBOL_OF_TUNARE, 1}},
            rewards = {
                {items.STRENGTHEN_BONE, 1},
                {items.OLD_PADDED_PANTS, 1}
            }
        }
    },
    [110110] = {
        [0] = {
            log = "Follow the riverbank north of Freeport to a broken bridge. At night Charlik Novandear may appear. Bring Corious his warm heart."
        },
        requirements = {{items.WARM_HEART, 1}},
        [1] = {log = "Purchase lantern oil from Merchant  Landi and return to Corious Slaerin."},
        [2] = {
            xp = 556753,
            requirements = {{items.LANTERN_OIL, 1}},
            rewards = {{items.DAGGER_OF_ESSENCE, 1}}
        }
    },
    [110113] = {
        rewards = {{items.COINS, 1}},
        [0] = {
            log = "Go see Agent Wilkenson. He is on the docks. ",
            requirements = {{items.COINS, 1}},
            rewards = {{items.NOTE_FROM_WILKINSON, 1}}
        },
        [1] = {
            log = "Go find Duminven. Look for him at Saerk's Tower.",
            requirements = {{items.NOTE_FROM_WILKINSON, 1}}
        },
        [2] = {
            log = "Head into Bastable village, locate the thief Eliene and follow her. Kill and loot both her and her contact. Return to Duminven.",
            requirements = {{items.CRACKED_RED_CRYSTAL, 1}}
        },
        [3] = {log = "Return to the Mark of Louhmanta to Corious."},
        [4] = {
            xp = 550698,
            rewards = {{items.WARD_DEATH, 1}}
        }
    },
    [110115] = {
        [0] = {log = "Go find Tailor Weynia, she can be found near the lighthouse, south of Freeport."},
        [1] = {
            log = "Search for and slay sidewinder snakes. Collect a skin and return it to Tailor Weynia.",
            requirements = {{items.SIDEWINDER_SKIN, 1}}
        },
        [2] = {
            log = "Search for and slay sand skippers. Collect a carapace and return it to Tailor Weynia.",
            requirements = {{items.SAND_SKIPPER_CARAPACE, 1}}
        },
        [3] = {
            log = "Search for and slay Gargantula. Collect a bundle of pristine silk and return it to Tailor Weynia.",
            requirements = {{items.BUNDLE_OF_PRISTINE_SILK, 1}}
        },
        [4] = {
            log = "Go to the west gate of Freeport and purchase vulture feathers from Dteven Savis and return to Tailor Weynia.",
            requirements = {{items.VULTURE_FEATHERS, 1}},
            rewards = {{items.POACHERS_LEGGINGS, 1}}
        },
        [5] = {log = "Go speak with Corious Slaerin."},
        [6] = {
            xp = 883791,
            requirements = {{items.POACHERS_LEGGINGS, 1}},
            rewards = {
                {items.DEATHWALKER_LEGGINGS, 1},
                {items.ENDURE_DISEASE, 1}
            }
        }
    },
    [110120] = {
        [0] = {log = "Go speak to Agent Wilkenson."},
        [1] = {log = "Go speak to Telina the Dark Witch."},
        [2] = {
            log = "A troll has your mark. Search southwest for the nasehir camps. Take the mark from Roj Eir Sew' Eil and return to Telina.",
            requirements = {{items.MARK_OF_LOUOTH, 1}}
        },
        [3] = {
            log = "Off the coast of MTG, slay skeletons and search the chests for the Chiseled Great Axe of Doom. Return to Telina the Dark Witch.",
            requirements = {{items.CHISELED_GREAT_AXE_OF_DOOM, 1}}
        },
        [4] = {
            log = "Go to Razor Back Fang mountain, southwest of Freeport. Find a waterlogged chest near an obelisk. Bring Telina the treasure.",
            requirements = {{items.ETCHED_HELMET_OF_GREATNESS, 1}}
        },
        rewards = {{items.NOTE_FROM_TELINA, 1}},
        [5] = {log = "Go speak to Corious Slaerin."},
        [6] = {
            xp = 2814929,
            rewards = {
                {items.SPACIOUS_MIND, 1},
                {items.MAGIC_SCEPTER, 1},
                {items.POWER_BOON, 1},
                {items.MAGIC_BOOK, 1}
            }
        }
    }
}
return quests