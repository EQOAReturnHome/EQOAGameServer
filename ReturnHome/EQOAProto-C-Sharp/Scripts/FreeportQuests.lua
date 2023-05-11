local items = require('Scripts/items')
--Magician(10) Human(0) Eastern(1)
local quests = {
    [100101] = {
        [0] = {log = "You must purchase an iron ring from Merchant Yulia, then return to Malsis."},
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
            requirements = {
                {items.CRACKED_ANT_PINCER, 2},
                rewards = {{items.SMOLDERING_AURA, 1}}
            }
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
                requirements = {{items.VENOM_SAC, 1}},
                rewards = {
                    {items.INFUSION, 1},
                    {items.BLACKENED_LEGGINGS, 1}
                }
            }
        },
        [100110] = {
            [0] = {log = "Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport."},
            [1] = {log = " Return to Guard Sareken with 147 tunar and three tough pike scales."},
            requirements = {{items.TOUGH_PIKE_SCALE, 3}},
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
            [0] = {log = "See Ilenar in the Shining Shield guild hall."},
            rewards = {{items.DAMAGED_ROBE, 1}},
            [1] = {
                log = " Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. "
            },
            requirements = {{items.DAMAGED_ROBE, 1}},
            [2] = {log = " Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger."},
            requirements = {{items.FINE_CHOCOLATES, 1}},
            [3] = {log = "Return to Delwin Stitchfinger."},
            rewards = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            [4] = {log = "Return the robe to Ilenar Crelwin."},
            requirements = {{items.CHOCOLATE_STAINED_ROBE, 1}},
            rewards = {{items.POISONED_BOX_OF_CHOCOLATES, 1}},
            [5] = {
                log = "Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says thanks."
            },
            requirements = {{items.POISONED_BOX_OF_CHOCOLATES, 1}},
            [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
            [7] = {
                xp = 550698,
                rewards = {{items.LAVA_WIND, 1}}
            }
        },
        [100115] = {
            [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
            [1] = {log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof."},
            requirements = {{items.BLOODFIN_TOOTH, 1}},
            rewards = {{items.NIGHTWORM_ROOTS, 1}},
            [2] = {log = "Return to Ilenar Crelwin."},
            requirements = {{items.NIGHTWORM_ROOTS, 1}},
            [3] = {log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood."},
            requirements = {{items.BLOOD_OF_DESERT_MADMEN, 1}},
            [4] = {log = "Return to Kellina."},
            [5] = {
                xp = 883791,
                rewards = {
                    {items.ENDURE_FIRE, 1},
                    {items.SUMMONERS_GARB, 1}
                }
            },
        [100120] = {
            [0] = {log = "You must speak to Ilenar about your mission."},
            [1] = {
                log = "Go find Crim Arikson. He is at the inn in the village of Bastable, which lies along the west road to Highpass Hold."
            },
            [2] = {log = "Go find Page Joseph Robert and offer to deliver his missives for him."},
            [3] = {log = "Bring the page of Sir Hanst to Crim Arikson."},
            requirements = {{items.PAGE_OF_SIR_HANST, 1}},
            rewards = {{items.FORGED_LETTER, 1}},
            [4] = {
                log = "The Amulet once belonged to Swiftwind Galeehart. Hangman's Hill is just east of Bobble-by-Water. Get that Amulet to Ilenar."
            },
            requirements = {{items.AMULET_OF_DECEPTION, 1}},
            [5] = {
                log = " Travel to the Temple of Light, west of Freeport. Present Leandro Novan with the forged letter."
            },
            requirements = {{items.FORGED_LETTER, 1}},
            [6] = {log = "Speak to Praetor Gunner about Geldwins Grimoire."},
            rewards = {{items.GELDWINS_GRIMOIRE, 1}},
            [7] = {log = "Bring the Grimoire to Ilenar."},
            requirements = {{items.GELDWINS_GRIMOIRE, 1}},
            [8] = {log = "Return to Kellina for your reward."},
            [9] = {
                xp = 2814929,
                rewards = {
                    {items.FROZEN_MARK, 1},
                    {items.MAGICAL_STAFF, 1}
                }
            },
            rewards = {
                {items.LAVA_STONE, 1},
                {items.MYSTICAL_TOME, 1}
            }
        }
    },
    --Enchanter(12) Human(0) Freeport(1)
    [120101] = {
        [0] = {log = "You must purchase Bronze Ring from Merchant Yulia, then return to Azlynn."},
        [1] = {xp = 430}
    },
    [120102] = {
        [0] = {log = "Go speak to Spiritmaster Keika."},
        [1] = {log = "Go speak to Coachman Ronks at the Stable."},
        [2] = {log = "Return to Corious Slaerin."},
        [3] = {xp = 2200}
    },
    [120103] = {
        [0] = {log = "Return two cracked ant pincers to Kellina"},
        [1] = {
            xp = 6900,
            rewards = {{item = 8373, qty = 1}},
            requirements = {{4866, 2}}
        }
    },
    [120104] = {
        [0] = {
            log = "Kellina needs a plain robe from Merchant Yulia, a silk cord from Merchant Yesam, and a ruined bat wing."
        },
        [1] = {
            xp = 17000,
            rewards = {{4927, 1}},
            requirements = {{5002, 1}, {8314, 1}, {4891, 1}}
        }
    },
    [120105] = {
        [0] = {
            log = "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Kellina."
        },
        [1] = {
            xp = 36500,
            rewards = {{8445, 1}},
            requirements = {{8321, 1}}
        }
    },
    [120106] = {
        [0] = {log = "Bring Kellina a chichan eel venom sac. You will find it north of Freeport, along the river."},
        [1] = {
            xp = 157474,
            rewards = {{8446, 1}, {8447, 1}},
            requirements = {{8332, 1}}
        }
    },
    [120107] = {
        [0] = {log = "Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport."},
        [1] = {log = "Return to Guard Sareken with 147 tunar and three tough pike scales."},
        [2] = {
            log = "Go investigate the old house north of Freeport for the orc ransacker, destroy it and bring Guard Sareken its club as proof."
        },
        [3] = {xp = 556753}
    },
    [120108] = {
        [0] = {log = "See Ilenar in the Shining Shield guild hall."},
        [1] = {
            log = "Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe."
        },
        [2] = {log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger."},
        [3] = {log = "Return to Delwin after a little while"},
        [4] = {log = "Return the robe to Ilenar Crelwin."},
        [5] = {
            log = "Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says thanks."
        },
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {xp = 550698}
    },
    [120109] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof and 260 tunar."},
        [2] = {log = "Return to Dagget Klem to buy your case of nightworm root."},
        [3] = {log = "Return to Ilenar Crelwin."},
        [4] = {log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood."},
        [5] = {log = "Return to Kellina."},
        [6] = {xp = 883791}
    },
    --Necromancer(11) Human(0) Freeport(1)
    [11010] = {
        [0] = {log = "You must purchase Bone Earring from Merchant Gilgash, then return to Corious Slaerin."},
        [1] = {xp = 430}
    },
    [11011] = {
        [0] = {log = "Go speak to Spiritmaster Keika."},
        [1] = {log = "Go speak to Coachman Ronks at the Stable."},
        [2] = {log = "Return to Corious Slaerin."},
        [3] = {xp = 2200}
    },
    [11012] = {
        [0] = {log = "Return two damaged dragonfly wings to Rathei Slaerin."},
        [1] = {
            xp = 6900,
            rewards = {{item = 8532, qty = 1}},
            requirements = {{4873, 2}}
        }
    },
    [11013] = {
        [0] = {
            log = "Rathei Slaerin needs a plain robe from Merchant Yulia, a silk cord from Merchant Yesam, and a sliver of snake meat"
        },
        [1] = {
            xp = 17000,
            rewards = {{4922, 1}},
            requirements = {{5002, 1}, {8314, 1}, {4879, 1}}
        }
    },
    [11014] = {
        [0] = {
            log = "Travel north from the north gate to the grassy area. Find the highwaymen and retrieve from them the stolen goods. Return to Rathei Slaerin."
        },
        [1] = {
            xp = 36500,
            rewards = {{8533, 1}},
            requirements = {{8321, 1}}
        }
    },
    [11015] = {
        [0] = {log = "Find Janxt in Temby to the north. Bring Corious Slaerin the cracked symbol of Tunare."},
        [1] = {
            xp = 157474,
            rewards = {{8534, 1}, {8525, 1}},
            requirements = {{8524, 1}}
        }
    },
    [11016] = {
        [0] = {
            log = "Follow the riverbank north of Freeport until you reach a broken bridge. Wait until nightfall and slay Charlik Novandear. Bring Corious his warm heart as proof."
        },
        [1] = {
            requirements = {{8526, 1}},
            log = "Purchase bottle of lantern oil from Merchant Landi and return to Corious Slaerin."
        },
        [2] = {
            xp = 556753,
            requirements = {{8431, 1}},
            rewards = {{8528, 1}}
        }
    },
    [11017] = {
        [0] = {log = "Go see  Agent Wilkenson. He is on the docks. "},
        [1] = {
            requirement = {{8433, 1}},
            log = "Go find Duminven. Look for him at Saerk's Tower."
        },
        [2] = {log = "Go see Grocer Fritz, purchase some fine chocolate and return to Delwin Stitchfinger."},
        [3] = {log = "Return to Delwin after a little while"},
        [4] = {log = "Return the robe to Ilenar Crelwin."},
        [5] = {
            log = "Give the special poisoned chocolate to Delwin Stitchfinger. Be sure to let him know Ilenar says thanks."
        },
        [6] = {log = "Return to Ilenar Crelwin. Tell him of Delwin's fate."},
        [7] = {xp = 550698}
    },
    [11018] = {
        [0] = {log = "Journey to Temby and arrange for nightworm roots through Dagget Klem."},
        [1] = {log = "Kill a bloodfin mother, bring Dagget Klem one of its teeth as proof and 260 tunar."},
        [2] = {log = "Return to Dagget Klem to buy your case of nightworm root."},
        [3] = {log = "Return to Ilenar Crelwin."},
        [4] = {log = "Find madmen at ruins south of Freeport. Return to Ilenar Crelwin with some of their blood."},
        [5] = {log = "Return to Kellina."},
        [6] = {xp = 883791}
    }
}

return quests
