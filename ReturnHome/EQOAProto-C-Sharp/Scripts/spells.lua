﻿local spells = {
    VITALITY=1,
    SOME_TAUNT_SPELL_TO_BE_FIX=2,
    ABOLISH_DEATH=3,
    ABSOLVING_WARD=4,
    ABSORB_AFFLICTION=5,
    ABSORB_ELEMENTS=6,
    ACCURACY=7,
    ACERBIC_MIXTURE=8,
    ACID_BLAST=9,
    ACIDIC_ADHESIVE=10,
    ACIDIC_ANCHOR=11,
    ACIDIC_BOND=12,
    ACIDIC_FASTENER=13,
    ACIDIC_HOLD=14,
    ACIDIC_MIXTURE=15,
    ACIDIC_STORM=16,
    ACIDIC_TEMPEST=17,
    ACROBATICS=18,
    ACUMEN=19,
    ADAMANT_STANCE=20,
    AERIALISTS_FORM=21,
    AFFLICTION=23,
    AGITATE=24,
    AGONIZE_UNDEAD=25,
    AGONIZED_SCREAM=26,
    AGO=27,
    AGONY=28,
    AHRIANNAS_BLESSED_BOON=29,
    AHRIANNAS_GENEROUS_GIFT=30,
    AIR_ELEMENTAL=31,
    AIR_ELEMENTALING=32,
    AIR_ELEMENTALKIN=33,
    AKTRANAS_CEREBRAL_FORTRESS=34,
    AKTRANAS_TOWER_OF_WILL=35,
    ALARMING_VISAGE=36,
    ALCHEMIC_CONDENSATION=37,
    ALCHEMICAL_ADHESIVE=38,
    ALCHEMICAL_ANCHOR=39,
    ALCHEMICAL_BOND=40,
    ALCHEMICAL_FASTENER=41,
    ALCHEMICAL_HOLD=42,
    ALLURE=43,
    AMBUSH=44,
    ANARCHY=45,
    ANCIENT_AFFLICTION=46,
    ANCIENT_DEATH=47,
    ANCIENT_MALADY=49,
    ANGUISHED_SCREAM=50,
    ANNIHILATE=51,
    ANNIHILATE_UNDEAD=52,
    ANTIDOTE=53,
    APPLIED_INTENT=55,
    ARCANE_BINDINGS=56,
    ARCANE_BOLT=58,
    ARCANE_BONDS=59,
    ARCANE_GROUNDING=60,
    ARCANE_SHACKLES=61,
    ARCANE_SHOCK=62,
    ARCH_FAMILIAR=63,
    ARCH_GUARDIAN=64,
    ARDENT_DEFENDER=65,
    ASPHYXIATION=66,
    ASSAIL=67,
    ASSAULT=68,
    ATONING_WARD=69,
    ATROPHIC_WIND=70,
    ATTENTION=71,
    AVALANCHE_OF_KARANA=72,
    AVATAR=73,
    AVENGE=74,
    AVOIDANCE=75,
    BACKSTAB=76,
    BADI=78,
    BADI_CRUSH=79,
    BADI_ELDER=80,
    BAGA_GATE=81,
    BAGA_PORTAL=82,
    BAGA_RASH=83,
    BAIT=84,
    BAITING_SHOUT=85,
    BAITING_TOUCH=86,
    BALANCED_STRIKE=87,
    BANDAGE=88,
    BARBCOAT=89,
    BARBSHIELD=92,
    BARRENS_COAST_PORTAL=94,
    BASH=95,
    BAT=96,
    BATTLECRY=97,
    BEAR_FORM=98,
    BELLOW=99,
    BIDDLEBYS_KNACK=100,
    BIDDLEBYS_FUNGAL_VISAGE=101,
    BIND_WOUNDS=102,
    BINDING_GRASP=103,
    BIXIE_FORM=104,
    BLADE_SWARM=106,
    BLADECOAT=107,
    BLAZE=109,
    BLAZES_DRAW=110,
    BLAZING_AURA=111,
    BLAZING_CLASH=112,
    BLESSED_BASTION=113,
    BLIGHT_OF_BERTOXXULOUS=114,
    BLIGHTED_BLADE=115,
    BLIGHTING_AURA=116,
    BLIGHTING_MIST=117,
    BLIZZARD=118,
    BLIZZARD_OF_KARANA=119,
    BLOOD_GALE=120,
    BLOOD_SQUALL=121,
    BLOOD_STORM=122,
    BLOOD_TEMPEST=123,
    BLOOD_VORTEX=124,
    BLOODGHOUL=125,
    BLOODLEECH=126,
    BLOODRAVEN=127,
    BLOODSTRIKE=128,
    BLOODWASP=129,
    BLOODWRAITH=130,
    BLOOMING_HEALTH=131,
    BOLDUNDERS_MOUNTAIN_ROOTS=133,
    BOLDUNDERS_PERSONAL_MINE=134,
    BONE_COLLECTI=135,
    BOOT=136,
    BOUNDLESS_MIND=137,
    BOUNDLESS_THOUGHT=138,
    BOUNTIFUL_FOCUS=139,
    BOUNTIFUL_GATHER_POWER=140,
    BOUNTIFUL_HARVEST=141,
    BOUNTIFUL_INFLUX=142,
    BRAMBLECOAT=143,
    BRAMBLESHIELD=145,
    BRAND_OF_AGONY=147,
    BRAND_OF_ANGUISH=148,
    BRAND_OF_PAIN=149,
    BRAND_OF_SORROW=150,
    BRAND_OF_WOE=151,
    BRAVERY=152,
    BRAWLING_BEAR=153,
    BREEZE=155,
    BRIARSHIELD=156,
    BRUTES_MASK=158,
    BURNING_ARROW=159,
    BURNING_AURA=160,
    BURNING_CLASH=161,
    BURNING_FLARE=162,
    BURNING_GAZE=163,
    BURNING_MARK=164,
    BURNING_PHASE_STAFF=165,
    BURNING_SHOT=166,
    BURNING_STRIKE=167,
    BURNOUT=168,
    BURST_OF_FLAME=169,
    BURST_OF_FROST=170,
    CAJOLE=171,
    CAJOLE_ANIMAL=172,
    CALEFACTION=174,
    CALIDUXS_EQUITABLE_DIVISION=176,
    CALIDUXS_INSATIABLE_APPETITE=177,
    CALL_LIGHTNING=178,
    CALL_OF_THE_HERO=179,
    CALL_TO_ACTION=180,
    CALL_TO_ARMS=181,
    CALL_TO_BATTLE=182,
    CALL_TO_COMBAT=183,
    CALL_TO_VICTORY=184,
    CALL_TO_WAR=185,
    CALLED_SHOT=186,
    CALM=187,
    CALTROPS=188,
    CAMOUFLAGE=189,
    CANNIBALIZE=191,
    CAPTIVATE=192,
    CAPTIVATE_ANIMAL=193,
    CATS_EASE=195,
    CATS_GRACE=196,
    CAUSTIC_MIXTURE=197,
    CELESTIAL_HEALING=198,
    CELESTIAL_LIGHT=199,
    CENSURE_DEATH=200,
    CENTER=201,
    CHAIN_LIGHTNING=202,
    CHANNEL=203,
    CHAOS_BOLT=204,
    CHAOTIC_FEEDBACK=205,
    CHAOTIC_FLUX=206,
    CHAOTIC_GLYPH=207,
    CHAOTIC_MARK=208,
    CHAOTIC_RUNE=209,
    CHAOTIC_SHOCK=210,
    CHARGE_COAT=211,
    CHARM=213,
    CHARM_ANIMAL=214,
    CHARM_UNDEAD=216,
    CHILLING_AURA=217,
    CHILLING_GAZE=218,
    CHOKE=219,
    CINDER_STORM=220,
    CIRCLE_OF_BARREN_COAST=221,
    CIRCLE_OF_DEAD_HIL=223,
    CIRCLE_OF_RATHE=224,
    CIRCLE_OF_STONEBRUNT=226,
    CLARITY=227,
    CLASH=228,
    CLAY_BARRIER=230,
    CLAY_GOLEM=231,
    CLEANSING_WARD=232,
    CLEAVE=233,
    CLEVER_THOUGHT=234,
    CLINGING_DARKNESS=235,
    CLOUD_OF_BERTOXXULOUS=236,
    COCKTAIL_OF_DEATH=237,
    COERCE=238,
    COERCE_ANIMAL=239,
    COERCE_UNDEAD=241,
    COMBUST=242,
    COMMAND_UNDEAD=243,
    COMMONS_GATE=244,
    COMPANION_SPIRIT=245,
    COMPRESS=246,
    CONCEAL=247,
    CONCENTRATION=248,
    CONCUSSION=250,
    CONSTITUTION=252,
    CONSUME_AFFLICTION=253,
    CONSUME_ELEMENTS=254,
    CONSUMING_MIST=255,
    CONTROL_UNDEAD=256,
    CONTROLLED_ANGER=257,
    CONTROLLED_DEFENSE=258,
    CONTROLLED_FORCE=259,
    CONTROLLED_FRENZY=260,
    CONTROLLED_RETREAT=261,
    CONTROLLED_WARDING=262,
    CORRODING_MIST=263,
    CORRODING_MIXTURE=264,
    CORROSIVE_JET=265,
    CORROSIVE_MIXTURE=266,
    CORROSIVE_SPRAY=267,
    CORRUPT=268,
    COURAGE=269,
    CRAWLING_HEALTH=270,
    CRAWLING_SKIN=272,
    CREATIVE_THINKING=273,
    CREATIVE_TRANSMUTATION=274,
    CREEPING_CRUD=275,
    CREEPING_HEALTH=277,
    CREMATION=279,
    CRIPPLED_ARMS=280,
    CRITICAL_ASSAULT=281,
    CRITICAL_ATTACK=282,
    CRITICAL_BARRAGE=283,
    CRITICAL_FLURRY=284,
    CRITICAL_STRIKE=285,
    CRUDE_QUICKENING=286,
    CRUDE_REFINEMENT=287,
    CRUDE_SUBLIMATION=288,
    CRUSADERS_AURA=289,
    CUNNING=290,
    CUNNING_INTENT=292,
    CURATIVE=293,
    CURE_MYSTICAL_PLAGUE=295,
    CURSE_OF_BERTOXXULOUS=296,
    CURSED_HA=297,
    CURSED_KISS=298,
    CURSED_TOUCH_OF_BERTOXXULOU=299,
    CURSED_TOWER=300,
    CUTTING_FURY=301,
    CYRCALIS_DEBILITATING_HEX=302,
    CYRCALIS_SINISTER_CANTRIP=303,
    DANCING_BLADES=304,
    DARING=306,
    DARK_COVENANT=307,
    DARK_PACT=308,
    DARKSTRIKE=309,
    DASH=310,
    DAZZLING_BLADES=311,
    DEADLY_BACKSTAB=313,
    DEADLY_GRASP=315,
    DEANS_KNACK=317,
    DEATH_MASTERY=318,
    DEATH_SCREAM=320,
    DEATHS_EMBRACE=321,
    DEATHS_KISS=323,
    DEATHSTRIKE=325,
    DECAY_MASTERY=326,
    DECAYING_FEVER=328,
    DECAYING_FLESH=330,
    DECAYING_WIND=332,
    DECIEVERS_MASK=333,
    DEFILE=334,
    DEFT_HANDS=335,
    DEFT_STRIKE=336,
    DEFTNESS=337,
    DEMENTED_VISIONS=338,
    DESPAIRING_SCREAM=339,
    DESPERATE_DEFENSE=340,
    DESSICATE=341,
    DESTROY_UNDEAD=342,
    DETONATE=343,
    DETONATING_MIXTURE=344,
    DIAMOND_STANCE=345,
    DIAMONDSKIN=346,
    DILIGENCE=347,
    DIRECT_INTENT=348,
    DISABLE_UNDEAD=349,
    DISCIPLES_AURA=350,
    DISCIPLES_CHANT=351,
    DISCIPLES_NIMBUS=352,
    DISCIPLES_RADIANCE=353,
    DISCIPLES_STRIKE=354,
    DISCORDANT_MIND=355,
    DISEASE_CLOUD=356,
    DISPEL_ARCANE=357,
    DISRUPT_DEATH=358,
    DISRUPT_UNDEAD=360,
    DISSIPATE=361,
    DISTILLATION=362,
    DISTRACTION=363,
    DIVINE_BLESSING=364,
    DIVINE_FAVOR=365,
    DIVINE_FERVOR=366,
    DIVINE_HEALING=367,
    DIVINE_INTERVENTION=368,
    DIVINE_MIGHT=369,
    DIVINE_PROTECTION=370,
    DIVINE_RECLAMATION=371,
    DIVINE_STRIKE=372,
    DIVINE_TOUCH=373,
    DIVINE_WISDOM=374,
    DIVINE_ZEAL=375,
    DIZZYING_BLADES=376,
    DJINN=378,
    DJINN_BOLT=379,
    DJINNI_VIZIER=380,
    DOMINATE_UNDEAD=381,
    DOOM_TOWER=382,
    DOOMING_DARKNESS=383,
    DOOMSTRIKE=384,
    DRAIN_FORTITUDE=385,
    DRAIN_INTELLECT=386,
    DRAIN_MIGHT=387,
    DRAIN_SPIRIT=388,
    DRAIN_STRENGTH=390,
    DRAW_MIGHT=391,
    DRAW_STRENGTH=392,
    DRIFTING_DEATH=393,
    DRONES_OF_DOOM=395,
    DROPKICK=397,
    EAGLE_EYE=398,
    EARTH_ELEMENTAL=399,
    EARTH_ELEMENTALING=400,
    EARTH_ELEMENTALKIN=401,
    EARTHERN_MARK=402,
    EBONWEAVE=403,
    EFREET=404,
    EFREETI_LORD=405,
    EFREETI_STRIKE=406,
    ELDER_FAMILIAR=407,
    ELDER_FORM=408,
    ELDER_GUARDIAN=409,
    ELDRICH_RUNE=410,
    ELECTRIC_DISCHARGE=411,
    ELECTRIC_PHASE_STAFF=412,
    ELECTRIFIED_BLADE=413,
    ELECTROCUTION=414,
    ELEMENTAL_BARRIER=415,
    ELEMENTAL_BULWARK=416,
    ELEMENTAL_DEFENSE=418,
    ELEMENTAL_GUARD=419,
    ELEMENTAL_MARK=422,
    ELEMENTAL_RESISTANCE=423,
    ELEMENTAL_SHELL=424,
    ELEMENTAL_SHIELD=425,
    ELEMENTAL_TOUGHNESS=427,
    ELEMENTAL_VEIL=428,
    ELEMENTAL_WARD=429,
    ELUDE=430,
    EMBER_PUNCH=431,
    EMBER_STRIKE=432,
    EMBERSPARK=433,
    EMBLAZE=435,
    EMPOWER=437,
    ENDURE_AFFLICTION=438,
    ENDURE_AILMENT=439,
    ENDURE_ARCANE=440,
    ENDURE_DISEASE=441,
    ENDURE_ELEMENTS=442,
    ENDURE_FIRE=443,
    ENDURING_AGITATE=444,
    ENDURING_BURNOUT=445,
    ENDURING_EMPOWER=446,
    ENDURING_ENERGIZE=447,
    ENDURING_EXCITE=448,
    ENDURING_MOTIVATE=449,
    ENDURING_VITALIZE=450,
    ENERGIZE=451,
    ENERGY_MAELSTROM=452,
    ENERGY_STORM=453,
    ENERVATE_DEATH=454,
    ENGULFING_DARKNESS=455,
    ENKINDLE=456,
    ENSHROUDING_DARKNESS=458,
    ENSNARE=459,
    ENSNAREMENT=461,
    ENTANGLEMENT=463,
    ENTICE=464,
    ENTICE_ANIMAL=465,
    ENVARIC_ACENSION=467,
    ENVARIC_SLA=468,
    ERODING_MIST=469,
    ESCAPE=470,
    ETHEREAL_HEALING=471,
    EVADE=472,
    EVASION=473,
    EXACTNESS=474,
    EXCITE=475,
    EXONERATING_WARD=476,
    EXORCISE=477,
    EXPANSIVE_MIND=478,
    EXPLODE=479,
    EXPLOSIVE_MIXTURE=480,
    EXPUNGE=481,
    FACILE_STRIKE=482,
    FAIRYS_EASE=483,
    FAIRYS_GRACE=484,
    FAITHS_BOON=485,
    FAITHS_RENEWAL=486,
    FAITHS_REVIVAL=487,
    FAITHS_REWARD=488,
    FAITHS_SUCCOR=489,
    FAITHFUL_HEALING=490,
    FALCON_EYE=491,
    FAMILIAR=492,
    FAULTY_REFINEMENT=493,
    FEEBLE_ARMS=494,
    FEET_LIKE_CAT=495,
    FERAL_SPIRIT=496,
    FERVENT_DEFENDER=497,
    FERVOR=498,
    FEY_MASK=499,
    FIELD_DRESS=501,
    FIERCE_STRIKE=502,
    FIERY_ANHILATI=503,
    FIERY_BURST=504,
    FIERY_DETONA=505,
    FIERY_MAELSTROM=506,
    FIERY_PEBBLE=507,
    FIERY_PUNCH=508,
    FIERY_STRIKE=509,
    FINESSE=510,
    FIRE_BOLT=511,
    FIRE_ELEMENTAL=512,
    FIRE_ELEMENTALING=513,
    FIRE_ELEMENTALKIN=514,
    FIRE_FIELD=515,
    FIRE_MAELSTROM=516,
    FIRE_SPRAY=517,
    FIRE_STORM=518,
    FIRST_AID=519,
    FLAME_ARROW=520,
    FLAME_KICK=521,
    FLAME_LICK=522,
    FLAME_PUNCH=523,
    FLAME_SHOT=524,
    FLAME_STRIKE=525,
    FLAMETONGUE=526,
    FLAMING_AURA=527,
    FLAMING_FURY=528,
    FLESH_MASTERY=529,
    FLOWERING_HEALTH=531,
    FOCUS=533,
    FOCUS__DESIRE=535,
    FOCUS_STRENGTH=536,
    FOCUSED_ATTENTION=537,
    FOCUSED_INTENT=538,
    FOCUSED_UMBRAL_CURSE=539,
    FORAGE_BERRIES=540,
    FORAGE_FRUIT=541,
    FORAGE_GRUB=542,
    FORAGE_HERB=543,
    FORAGE_TUBER=544,
    FORCE_KICK=545,
    FORCED_MARCH=546,
    FORESTS_BOON=547,
    FORESTS_BOUNTY=548,
    FORESTS_GIFT=549,
    FORESTS_MIGHT=550,
    FORESTS_RELIEF=551,
    FORESTS_SUCCOR=552,
    FORM_OF_THE_GREAT_BEAR=553,
    FORM_OF_THE_HUNTER=554,
    FORM_OF_THE_MASTER=556,
    FORM_OF_THE_MAULER=557,
    FORM_OF_THE_PREDATOR=558,
    FORTIFYING_AGENT=559,
    FORTITUDE=560,
    FRANTIC_DEFENSE=561,
    FREEPORT_COCKTAIL=562,
    FREEPORT_EVACUATION=563,
    FREEZE=564,
    FREEZING_ARROW=565,
    FREEZING_CLASH=566,
    FREEZING_MIXTURE=567,
    FREEZING_PUNCH=568,
    FREEZING_SHOT=569,
    FREEZING_STRIKE=570,
    FRENZIED_DEFENSE=572,
    FRENZIED_SPIRIT=573,
    FRENZY=574,
    FROST_ARROW=575,
    FROST_ORB=576,
    FROST_PUNCH=577,
    FROST_SHOT=578,
    FROST_STRIKE=579,
    FROSTS_DRAW=580,
    FROZEN_MARK=581,
    FULMINANT_MIXTURE=582,
    FUNERAL_PYRE=583,
    FURIOUS_ASSAULT=584,
    FURIOUS_ATTACK=585,
    FURIOUS_BARRAGE=586,
    FURIOUS_DEFENSE=587,
    FURIOUS_FLURRY=588,
    FURIOUS_STRIKE=589,
    FUROR=590,
    FUSION=591,
    GALE_OF_POISON=592,
    GARGOYLE_FAMILIAR=593,
    GASPING_EMBRACE=594,
    GATHER_POWER=595,
    GATHER_SHADOWS=596,
    GELLEDYNS_FERAL_IMITATION=598,
    GELLEDYNS_PERFECT_VISION=599,
    GHASTLY_VISAGE=600,
    GHILAN=601,
    GHILAN_NOBLE=602,
    GHILAN_WASH=603,
    GHOSTLY_DEATH=604,
    GLACIAL_FLOW=605,
    GLACIAL_STREAM=606,
    GLACIAL_TORRENT=607,
    GLACIATION=608,
    GLAMOUR_BROWN=609,
    GLEAMING_BASTION=610,
    GLIMMERING_BLADES=611,
    GLOOM_COVENANT=612,
    GLOOM_PACT=613,
    GLORIOUS_BAIT=614,
    GLORIOUS_GOAD=615,
    GLORIOUS_INCITE=616,
    GLORIOUS_PROVOKE=617,
    GLORIOUS_TAUNT=618,
    GOAD=619,
    GOADING_SHOUT=620,
    GOADING_TOUCH=621,
    GRAALS_FINAL_STRIKE=622,
    GRAALS_VENOMOUS_STRIKE=623,
    GRAND_ANIMATION=624,
    GRANITE_BARRIER=625,
    GREATER_AFFLICTION=626,
    GREATER_ANTIDOTE=627,
    GREATER_BACKSTAB=629,
    GREATER_CAMOUFLAGE=631,
    GREATER_CANNIBALIZE=634,
    GREATER_CURATIVE=635,
    GREATER_DISTILLATION=636,
    GREATER_ENERGY_STORM=637,
    GREATER_FAMILIAR=638,
    GREATER_FIRE_MAELSTROM=639,
    GREATER_FIRE_STORM=640,
    GREATER_GUARDIAN=641,
    GREATER_HEALING=642,
    GREATER_ICE_MAELSTROM=643,
    GREATER_ICE_STORM=644,
    GREATER_INFUSION=645,
    GREATER_MALADY=646,
    GREATER_PURIFICATION=647,
    GREATER_PURITY=648,
    GREATER_REFINEMENT=650,
    GREVIOUS_WOUND=651,
    GRIM_TOWER=652,
    GROUP_ABSORB_AFFLICTION=653,
    GROUP_ABSORB_ELEMENTS=654,
    GROUP_ACCURACY=656,
    GROUP_ACUMEN=657,
    GROUP_APPLICATION=658,
    GROUP_BRAVERY=659,
    GROUP_CENTER=660,
    GROUP_CLEVERNESS=661,
    GROUP_CONSUME_AFFLICTION=662,
    GROUP_CONSUME_ELEMENTS=663,
    GROUP_COURAGE=665,
    GROUP_CREATIVITY=666,
    GROUP_CUNNING=667,
    GROUP_DARING=668,
    GROUP_DEFTNESS=669,
    GROUP_DIAMONDSKIN=670,
    GROUP_DIRECTION=672,
    GROUP_ENDURE_AFFLICTION=673,
    GROUP_ENDURE_AILMENT=674,
    GROUP_ENDURE_ELEMENTS=675,
    GROUP_EXACTNESS=676,
    GROUP_FOCUS=677,
    GROUP_INGENUITY=678,
    GROUP_INSPIRATION=679,
    GROUP_INTENT=680,
    GROUP_NATURESKIN=681,
    GROUP_ORDER=683,
    GROUP_PRECISION=684,
    GROUP_PRODIGIOUSNESS=685,
    GROUP_RESIST_AFFLICTION=686,
    GROUP_RESIST_ELEMENTS=687,
    GROUP_RESOLUTION=689,
    GROUP_ROCKSKIN=690,
    GROUP_STEELSKIN=692,
    GROUP_TRIBAL_FORCE=694,
    GROUP_TRIBAL_MIGHT=695,
    GROUP_TRIBAL_STRENGTH=696,
    GROUP_TRIBAL_THEW=697,
    GROUP_TUNARES_BLESSING=698,
    GROUP_VALOR=700,
    GROUP_WARD_AFFLICTION=701,
    GROUP_WARD_ELEMENTS=702,
    GROUP_WOLF_FORM=704,
    GROUP_WOODSKIN=706,
    GUARD=707,
    GUARDIAN=708,
    GUILE=709,
    GYMNASTS_ACUITY=711,
    HAIL_STORM=713,
    HALLOWED_BASTION=714,
    HALLOWED_MIGHT=715,
    HALLOWED_ZEAL=716,
    HAND_OF_BERTOXXULOUS=717,
    HAND_OF_HATE=718,
    HAND_OF_INNORUUK=719,
    HAND_OF_SPITE=720,
    HARA=721,
    HARDENING_AGENT=722,
    HARM_TOUCH=723,
    HARM_UNDEAD=724,
    HARNESS_STRENGTH=725,
    HARVEST=726,
    HASTED_BLADE=727,
    HATEWEAVE=728,
    HATRED=729,
    HAUNTING_VISAGE=730,
    HAWK_EYE=731,
    HEAL=732,
    HEALING=733,
    HEAVY_ARMS=734,
    HEIGHTENED_MIND=735,
    HEIST=736,
    HERO_GUARD=737,
    HERO_GUARD_II=738,
    HERO_GUARD_III=739,
    HERO_STRIKE=740,
    HEROS_BLOW=741,
    HIDE=742,
    HIGHPASS_EVACUATION=743,
    HOLY_AID=744,
    HOLY_BLESSING=745,
    HOLY_BOLT=746,
    HOLY_FAVOR=747,
    HOLY_FERVOR=748,
    HOLY_MIGHT=749,
    HOLY_SHOCK=750,
    HOLY_TOUCH=751,
    HOLY_WORD=752,
    HOLY_ZEAL=753,
    HONED_INTENT=754,
    HOWL=755,
    HOWLING_DEATH=756,
    HOWLING_MASK=758,
    HOWLING_SPIRITS=760,
    HULKING_BONES=761,
    HUNTERS_INSTINCT=762,
    HUNTERS_SKILL=763,
    ICE_COMET=764,
    ICE_FIELD=765,
    ICE_MAELSTROM=766,
    ICE_SHARD=767,
    ICE_SPRAY=768,
    ICE_STORM=769,
    ICHOR_SPRAY=770,
    ICY_ANNIHILATE=771,
    ICY_DETONATE=772,
    ICY_EXPLODE=773,
    ICY_FURY=774,
    ICY_MIXTURE=775,
    ICY_NOVA=776,
    ICY_SUPERNOVA=777,
    IGNITE_BONES=778,
    ILLUSION_BARBARIAN=779,
    ILLUSION_BROWNIE=780,
    ILLUSION_CLAY_GOLEM=781,
    ILLUSION_DARK_ELF=782,
    ILLUSION_DWARF=783,
    ILLUSION_ELF=784,
    ILLUSION_ERUDITE=785,
    ILLUSION_GNOME=786,
    ILLUSION_HALFLING=787,
    ILLUSION_HUMAN=788,
    ILLUSION_IRON_GOLEM=789,
    ILLUSION_OGRE=790,
    ILLUSION_STONE_GOLEM=791,
    ILLUSION_TROLL=792,
    IMPACT=793,
    IMPART=795,
    IMPROVED_INVISIBILITY=796,
    INCINERATING_AURA=800,
    INCINERATION=801,
    INCITE=802,
    INCITING_SHOUT=803,
    INCITING_TOUCH=804,
    INFECTIOUS_AURA=805,
    INFECTIOUS_SPRAY=806,
    INFECTIOUS_STREAM=807,
    INFERNAL_BO=808,
    INFERNAL_COVENANT=809,
    INFERNAL_PACT=810,
    INFERNO=811,
    INFERNOS_DRAW=812,
    INFINITE_MIND=813,
    INFLAME=814,
    INFLUX=816,
    INFUSE=817,
    INFUSION=818,
    INGENIOUS_THOUGHT=820,
    INNER_FIRE=821,
    INNER_STRENGTH=822,
    INNOTHULE_FEVER=823,
    INSECT_SWARM=824,
    INSIDIOUS_WASH=826,
    INSPIRATION=827,
    INSPIRED_THINKING=829,
    INTUITION=830,
    INVENTORS_KNACK=831,
    INVISIBILITY=832,
    INVOKE_LIGHTNING=834,
    IRON_CONVICTION=835,
    IRON_GOLEM=836,
    IRON_RESOLUTION=837,
    IRON_RESOLVE=838,
    IRON_SKIN=839,
    IRON_STANCE=840,
    IRON_WILL=841,
    JAGGEDPINE_CIRCLE=842,
    JAKAIS_INVIGORATING_BELLOW=844,
    JAKAIS_RALLYING_CRY=845,
    JENTYLIS_LUST_FOR_POWER=846,
    JENTYLIS_OVERARCHING_GREED=847,
    JOLT=848,
    JOLTING_DISCHARGE=849,
    JUSTICE=850,
    KELINARS_GATE=851,
    KICK=852,
    KINDRED_SPIRIT=854,
    KINETIC_MIXTURE=855,
    KITHACOR_CIRCLE=856,
    KLIKANON_COCKTAIL=858,
    KNIGHTS_BASH=859,
    KNIT=860,
    LARGESSE_OF_PLAGUE=861,
    LAVA_BURST=862,
    LAVA_FLOW=863,
    LAVA_SHOCK=864,
    LAVA_STONE=865,
    LAVA_STREAM=866,
    LAVA_STRIKE=867,
    LAVA_WIND=868,
    LAY_HANDS=869,
    LESSER_ANTIDOTE=870,
    LESSER_BACKSTAB=872,
    LESSER_CANNIBALIZE=874,
    LESSER_CURATIVE=875,
    LESSER_DISTILLATION=877,
    LESSER_FAMILIAR=878,
    LESSER_GUARDIAN=879,
    LESSER_INFUSION=880,
    LESSER_MALADY=881,
    LESSER_PURIFICATION=882,
    LESSER_PURITY=883,
    LESSER_WOUND=885,
    LICH=886,
    LIFE_CLAMP=887,
    LIFE_CLENCH=888,
    LIFE_CLUTCH=889,
    LIFE_DRAW=890,
    LIFE_GRASP=892,
    LIFE_GRIP=893,
    LIFE_TAP=894,
    LIFES_BANE=895,
    LIFESPIKE=897,
    LIFT=899,
    LIGHT_HEALING=900,
    LIGHTNING_ARROW=901,
    LIGHTNING_BLAST=902,
    LIGHTNING_CALL=903,
    LIGHTNING_COAT=904,
    LIGHTNING_COLUMN=906,
    LIGHTNING_DISCHARGE=907,
    LIGHTNING_KICK=908,
    LIGHTNING_MAELSTROM=909,
    LIGHTNING_PUNCH=910,
    LIGHTNING_SHOT=911,
    LIGHTNING_STORM=912,
    LIGHTNING_STRIKE=913,
    LIMITLESS_MIND=915,
    LOATHSOME_COVENANT=916,
    LOATHSOME_PACT=917,
    LUCIDITY=918,
    LULL=919,
    LUMBERING_ARMS=920,
    LUMBERING_BONES=921,
    LUNGING_MANTIS=922,
    LUPINE_GUISE=923,
    MAGIKOTS_GOOD_BREEDING=925,
    MAGIKOTS_SACRIFICAL_TRAINING=926,
    MAGMA_STORM=927,
    MAIADRAS_STEADFAST_ASSISTANCE=928,
    MAIADRAS_UNRELENTING_HOPE=930,
    MAJOR_ANTIDOTE=932,
    MAJOR_CANNIBALIZE=934,
    MAJOR_CURATIVE=935,
    MAJOR_DISTILLATION=936,
    MAJOR_INFUSION=937,
    MAJOR_PURIFICATION=938,
    MAJOR_PURITY=939,
    MALADY=941,
    MALICE=942,
    MALICEWEAVE=943,
    MANA_BLAST=944,
    MANA_BURST=946,
    MANA_FLOW=947,
    MANABURN=948,
    MANTICORE_TAIL=949,
    MARBLE_BARRIER=950,
    MARK_OF_AGONY=951,
    MARK_OF_ANGUISH=952,
    MARK_OF_FAITH=953,
    MARK_OF_PAIN=954,
    MARK_OF_SORROW=955,
    MARK_OF_WOE=956,
    MASTER_OF_DISGUISE=957,
    MASTERFUL_STRIKE=958,
    MASTERY_OF_PLAGUE=959,
    MEGAMETAMILIPIZIONS_CLUTCH_WARD=960,
    MELINIS_HARMONIUS_THOUGHTS=961,
    MELINIS_TRANQUIL_STANCE=962,
    MELTON_EVACUATION=963,
    MELTON_GATE=964,
    MELTON_PORTAL=965,
    MEND=966,
    MENTAL_BOOST=967,
    MENTAL_FOCUS=968,
    MENTAL_SURGE=969,
    METEORIC_MIXTURE=970,
    MIND__BODY=971,
    MINOR_ANTIDOTE=972,
    MINOR_BACKSTAB=975,
    MINOR_BLESSING=977,
    MINOR_CANNIBALIZE=978,
    MINOR_CURATIVE=979,
    MINOR_DISTILLATION=980,
    MINOR_ENERGY_STORM=981,
    MINOR_FAMILIAR=982,
    MINOR_FIRE_STORM=983,
    MINOR_HEALING=984,
    MINOR_ICE_STORM=985,
    MINOR_INFUSION=986,
    MINOR_MALADY=987,
    MINOR_PURIFICATION=988,
    MINOR_PURITY=989,
    MINOR_SWARM=992,
    MINOR_WOUND=993,
    MIRACULOUS_MIXTURE=994,
    MIRCYLS_ANIMATION=995,
    MISE=996,
    MIST=997,
    MITHRIL_SKIN=998,
    MORADHIM_COCKTAIL=999,
    MORTAL_MASK=1000,
    MORTAL_WOUND=1001,
    MOTIVATE=1002,
    MUG=1003,
    MURDEROUS_BACKSTAB=1004,
    MUSCLE_LOCK=1006,
    NATURE_FORM=1007,
    NATURE_WALK=1009,
    NATURES_BLESSING=1011,
    NATURES_BOON=1012,
    NATURES_CLOAK=1013,
    NATURES_GUISE=1014,
    NATURES_MANTLE=1016,
    NATURES_RENEWAL=1017,
    NATURES_WISDOM=1018,
    NATUREBLADE=1019,
    NATURESKIN=1020,
    NERIAK_COCKTAIL=1021,
    NERIUS_GATE=1022,
    NERIUS_PORTAL=1023,
    NIFES_WARDING=1024,
    NIMBLE_EASE=1025,
    NIMBLE_GRACE=1026,
    NIMBLE_STRIKE=1660,
    NORTH_RO_GATE=1027,
    NORTH_RO_PORTAL=1028,
    NOVA=1029,
    NOXIOUS_BLADE=1030,
    OKEILS_RADIATION=1031,
    OKEILS_EMBERS=1032,
    ODUS_GATE=1033,
    ODUS_PORTAL=1034,
    ORDER=1035,
    OZMOS_CAREFUL_TEASING=1036,
    OZMOS_ECHOING_MOCKERY=1037,
    OZMOS_GOADING_RIDICULE=1038,
    OZMOS_INFURIATING_JEER=1039,
    OZMOS_MATERNAL_ASPERATION=1040,
    OZMOS_UNRELENTING_RIDICULE=1041,
    PACK_SPIRIT=1042,
    PAIN_TOUCH=1045,
    PAINSTRIKE=1047,
    PARALYTIC_PUNISHME=1048,
    PEACE__HARMONY=1049,
    PENDRILS_ANIMATION=1050,
    PENETRATING_BLOW=1051,
    PERCEPTION=1052,
    PERFORMERS_FINESSE=1053,
    PERSECUTE_DEATH=1054,
    PERSECUTE_UNDEAD=1056,
    PET_ENDURE_FIRE=1057,
    PET_ENDURE_POISON=1058,
    PETRIFYING_AGENT=1059,
    PHASE_RING=1060,
    PICKPOCKET=1061,
    PILFER=1062,
    PILLAR_OF_DEADS=1063,
    PILLAR_OF_FLAME=1064,
    PILLAR_OF_FORCE=1065,
    PILLAR_OF_HEROICS=1066,
    PILLAR_OF_MIGHT=1067,
    PILLAR_OF_STRENGTH=1068,
    PIOUS_MIGHT=1069,
    PIOUS_ZEAL=1070,
    PLAGUE=1071,
    PLAGUE_OF_BERTOXXULOUS=1072,
    PLAGUEBRINGERS_RASH=1073,
    POISON_ARROW=1074,
    POISON_DART=1075,
    POISON_FIELD=1076,
    POISON_SHOT=1077,
    POISON_SPRAY=1078,
    POISONED_BLADE=1079,
    POUNCING_TIGER=1081,
    POWER_FLOOD=1084,
    POWER_FLOW=1085,
    POWER_FLUX=1086,
    POWER_GALE=1087,
    POWER_INDUCTI=1088,
    POWER_JET=1089,
    POWER_SQUALL=1090,
    POWER_STORM=1091,
    POWER_STREAM=1092,
    POWER_SURGE=1093,
    POWER_TEMPEST=1094,
    POWER_TIDE=1095,
    POWER_VORTEX=1096,
    POWER_WAVE=1097,
    PRECISION=1098,
    PREDACIOUS_INSTINCT=1099,
    PREDACIOUS_SKILL=1100,
    PREDATORIAL_INSTINCT=1101,
    PREDATORIAL_SKILL=1102,
    PRESERVED_FLESH=1103,
    PRIMAL_AFFLICTION=1105,
    PRIMAL_MALADY=1106,
    PRIMAL_SPIRIT=1107,
    PRODIGIOUS_THOUGHT=1108,
    PROFESSORS_KNACK=1109,
    PROTECTION_OF_KARANA=1110,
    PROVOKE=1111,
    PROVOKING_SHOUT=1112,
    PROVOKING_TOUCH=1113,
    PUNISH_DEATH=1114,
    PUNT=1115,
    PURE_REFINEMENT=1116,
    PURIFICATION=1117,
    PURITY=1118,
    PUTRID_FLESH=1120,
    QEYNOS_COCKTAIL=1122,
    QUICK_BLADE=1123,
    QUICK_HANDS=1124,
    QUICK_PUNCH=1125,
    QUICK_STITCHING=1126,
    QUICK_STRIKE=1127,
    QUICKENING=1129,
    QUILEYNES_CRUEL_EMBRACE=1130,
    QUILEYNES_UNSPEAKABLE_HUNGER=1131,
    RABID_INFECTION=1132,
    RAKISH_ASSAULT=1133,
    RAKISH_JAB=1134,
    RAKISH_LUNGE=1135,
    RAKISH_STRIKE=1136,
    RAKISH_THRUST=1137,
    RAMPAGE=1138,
    RAPID_STRIKE=1139,
    RASHIROZS_PERFECT_EVASION=1140,
    RASHIROZS_PERSISTENT_VANISHING=1141,
    RATHE_MOUNTAINS_PORTAL=1142,
    RAVEN_EYE=1143,
    REANIMATE=1144,
    REBUKE_DEATH=1145,
    RECKLESS_ANGER=1146,
    RECKLESS_FORCE=1147,
    RECKLESS_FRENZY=1148,
    RECLAMATION=1149,
    RECONSTRUCTIVE=1150,
    REFINEMENT=1151,
    REGENERATIVE_AURA=1152,
    REINFORCE_BONE=1154,
    REMEDY=1155,
    REMOVE_MASK=1156,
    REND=1157,
    RENDING_FURY=1158,
    RENEW=1159,
    RENEWAL=1162,
    RESIST_AFFLICTION=1163,
    RESIST_ELEMENTS=1164,
    RESIST_POISON=1165,
    RESOLUTION=1166,
    RESURRECTION=1167,
    RESUSCITATE=1168,
    RETRIBUTION=1169,
    REVIVE=1170,
    REVIVISCENCE=1171,
    RIGHTEOUS_BLOW=1172,
    RIGHTEOUS_FURY=1173,
    RIGHTEOUS_STRIKE=1174,
    RIGHTEOUS_VENGEANCE=1175,
    RIGHTEOUS_WRATH=1176,
    RIPPING_FURY=1177,
    ROAR=1178,
    ROARING_DRAGON=1179,
    ROARING_MASK=1181,
    ROCKSKIN=1182,
    RODCETS_TOUCH=1183,
    ROGUES_DAGGERS=1184,
    ROOT=1185,
    ROTTING_FLESH=1188,
    RUDDISHS_INSPIRING_PIETY=1190,
    RUDDISHS_UNASSAILABLE_FAITH=1191,
    RUNE_OF_ANIMATION=1192,
    RUNE_OF_KARANA=1193,
    RUSH=1194,
    SACRED_BASTION=1195,
    SACRED_HEART=1196,
    SACRED_SHIELD=1197,
    SALVE=1198,
    SANITY_WARP=1199,
    SAVVY=1200,
    SCHOLARS_MASK=1202,
    SCORIA_STONE=1203,
    SCORIAE=1204,
    SCOURING_MIST=1205,
    SCREAM_OF_AGONY=1206,
    SCREAM_OF_ANGUISH=1207,
    SCREAM_OF_DESPAIR=1208,
    SCREAM_OF_PAIN=1209,
    SCREAM_OF_TORMENT=1210,
    SEARING_AURA=1211,
    SEPTIC_MIXTURE=1212,
    SERPENTS_EASE=1213,
    SERPENTS_GRACE=1214,
    SEVERUPIDUS_COCKTAIL_OF_TORMENT=1215,
    SHADOW_COVENANT=1216,
    SHADOW_PACT=1217,
    SHADOW_TOWER=1218,
    SHADOW_TUNIC=1219,
    SHADOW_WALK=1220,
    SHALEES_ANIMATION=1221,
    SHAMBLING_BONES=1222,
    SHARDS_OF_THE_HUNTER=1223,
    SHIELD_OF_1000_NEEDLES=1224,
    SHIMMERING_BLADES=1226,
    SHINING_BASTION=1228,
    SHOCK=1229,
    SHOCK_OF_ACID=1231,
    SHOCK_OF_FROST=1232,
    SHOCK_OF_POISON=1233,
    SHOCK_OF_SPIRITS=1234,
    SHOCK_OF_SPORES=1235,
    SHOCKBLADE=1236,
    SHOCKING_CLASH=1237,
    SHOCKING_DISCHARGE=1238,
    SHOCKING_GAZE=1239,
    SHOCKING_MARK=1240,
    SHOCKING_STRIKE=1241,
    SHRIEK_OF_DREAD=1242,
    SHRIEK_OF_HATE=1243,
    SHRIEK_OF_HORROR=1244,
    SHRIEKING_SPIRITS=1245,
    SHROUD_OF_DEATH=1246,
    SICKENING_WIND=1247,
    SILENT_BLADES=1248,
    SIPHON_FORTITUDE=1249,
    SIPHON_INTELLECT=1250,
    SIPHON_LIFE=1251,
    SIPHON_MIGHT=1253,
    SIPHON_STRENGTH=1254,
    SIPHON_VITALITY=1256,
    SKY_PORT=1257,
    SKY_PORTAL=1258,
    SLAM=1259,
    SLASH=1260,
    SLASHING_FURY=1261,
    SLEET=1262,
    SLEET_SHOCK=1263,
    SLEET_STRIKE=1264,
    SMITE=1265,
    SMITE_DEATH=1266,
    SMITE_UNDEAD=1268,
    SMOKING_AURA=1270,
    SMOLDERING_AURA=1271,
    SNARE=1272,
    SNEAK_ATTACK=1273,
    SOOTHE=1274,
    SOOTHING_VISAGE=1275,
    SORROWSTRIKE=1276,
    SOTHIMUS_ARTITERAL_TARGETTING=1277,
    SOTHIMUS_CRITICAL_WOUNDING=1278,
    SOUL_MASTERY=1279,
    SPACIOUS_MIND=1281,
    SPARROW_EYE=1282,
    SPIKECOAT=1283,
    SPIKESHIELD=1285,
    SPINNING_KICK=1287,
    SPIRIT__BEING=1288,
    SPIRIT_ARMOR=1289,
    SPIRIT_BLAST=1290,
    SPIRIT_BOLT=1291,
    SPIRIT_CIRCLE=1292,
    SPIRIT_FORM=1295,
    SPIRIT_OF_THE_WOLF=1297,
    SPIRIT_SHIELD=1299,
    SPIRIT_SLAM=1300,
    SPIRIT_STRIKE=1301,
    SPIRIT_TAP=1302,
    SPIRIT_WALK=1304,
    SPIRITS_BOON=1305,
    SPIRITUAL_GUARD=1306,
    SPIRITUAL_GUIDE=1307,
    SPIRITUAL_HUNTER=1308,
    SPIRITUAL_RENEWAL=1309,
    SPIRITUAL_SLAYER=1310,
    SPIRITUAL_WARRIOR=1311,
    SPITEWEAVE=1312,
    SPRINT=1313,
    SPRITE_FORM=1315,
    SSLATHIS_GATE=1317,
    STAGGERING_SHOT=1318,
    STALKERS_INSTINCT=1319,
    STALKERS_SKILL=1320,
    STAR_FIRE=1321,
    STASIS_STRIKE=1322,
    STATEGIC_BLOW=1323,
    STATIC_ANNIHILATE=1324,
    STATIC_ARROW=1325,
    STATIC_BOLT=1326,
    STATIC_CLASH=1327,
    STATIC_COAT=1328,
    STATIC_DETONATE=1330,
    STATIC_DISCHARGE=1331,
    STATIC_EXPLODE=1332,
    STATIC_FIELD=1333,
    STATIC_NOVA=1334,
    STATIC_PUNCH=1335,
    STATIC_SHOCK=1336,
    STATIC_SHOT=1337,
    STATIC_SPRAY=1338,
    STATIC_STRIKE=1339,
    STATIC_SUPERNOVA=1340,
    STEAL=1341,
    STEEL_BARRIER=1342,
    STEEL_SKIN=1343,
    STEEL_STANCE=1344,
    STEELSKIN=1345,
    STINGING_SLEET=1346,
    STINGING_SWARM=1347,
    STOMP=1349,
    STONE_GOLEM=1350,
    STONE_STANCE=1351,
    STONEBRUNT_PORTAL=1352,
    STORM_CALLERS_CURSE=1353,
    STORM_COAT=1354,
    STORM_OF_BLADES=1355,
    STORMBLADE=1356,
    STORMLORDS_RUNE=1357,
    STORMLORDS_TOUCH=1358,
    STRANGLING_HOLD=1359,
    STRENGTHEN_BONE=1360,
    STRIKE=1361,
    STRIKEBLADE=1362,
    STRIKING_ADDER=1363,
    STRIKING_ASP=1364,
    STRIKING_COBRA=1365,
    STRIKING_COUATL=1366,
    STRIKING_VIPER=1367,
    STUDENTS_KNACK=1368,
    STUMBLING_BONES=1369,
    STUMBLING_FEVER=1370,
    STUPEFY=1371,
    SUBLIMATION=1373,
    SUBVERSION=1374,
    SUFFERING=1375,
    SUFFOCATE=1376,
    SUFFOCATING_SPHERE=1377,
    SUMMON_SERVANT=1378,
    SUMMON_BITTER_DRAFT=1379,
    SUMMON_CITRUS_MIXTURE=1380,
    SUMMON_FINE_RED_WINE=1381,
    SUMMON_GINGER_ROOT_ALE=1382,
    SUMMON_SPARKLING_CIDER=1383,
    SUMMON_STRAWBERRY_LEMONADE=1384,
    SUMMON_UNFILTERED_WATER=1385,
    SUMMON_WEAK_ALE=1386,
    SUNDER=1387,
    SUNSTRIKE=1388,
    SUPERIOR_DISTILLATION=1389,
    SUPERIOR_HEALING=1390,
    SUPERIOR_INVISIBILITY=1391,
    SUPERIOR_REFINEMENT=1394,
    SUPERNOVA=1395,
    SWARMING_BONES=1396,
    SWARMING_DEATH=1398,
    SWIFT_BLADE=1400,
    SWOOPING_EAGLE=1401,
    SYCAMORE_CIRCLE=1403,
    SYNDERS_BURNING_HATRED=1405,
    SYNDERS_BURNING_MINDSET=1406,
    TAINTEDBLADE=1407,
    TAKE_A_SHIELD_BREAK=1408,
    TALIMUSS_ADAMANTINE_BARRIER=1409,
    TALIMUSS_DIAMOND_BARRIER=1410,
    TANGLING_ROOTS=1411,
    TANGLING_VINES=1412,
    TANGLING_WEEDS=1413,
    TANGLING_WILD=1414,
    TAP_FORTITUDE=1415,
    TAP_INTELLECT=1416,
    TAP_STRENGTH=1417,
    TAUNT=1418,
    TAUNTING_SHOUT=1420,
    TAUNTING_TOUCH=1421,
    TEACHERS_KNACK=1422,
    TEARING_FURY=1423,
    TELEDICS_CHILLING_BREATH=1424,
    TELEDICS_FROZEN_HEART=1425,
    TEMPEST=1426,
    TENEBROUS_COVENANT=1427,
    TENEBROUS_PACT=1428,
    TEWKS_GUARD=1429,
    THEATER_BIZZARE_PORTAL=1430,
    THISTLECOAT=1431,
    THISTLESHIELD=1433,
    THORNSHIELD=1434,
    THORNY_ROOTS=1436,
    THORNY_VINES=1437,
    THORNY_WEEDS=1438,
    THORNY_WILD=1439,
    THUNDER_COAT=1440,
    THUNDERBOLT=1442,
    THUNDEROUS_FURY=1443,
    TIDE_OF_GLORY=1444,
    TIDE_OF_HEALTH=1445,
    TIDE_OF_LIFE=1446,
    TIDE_OF_RECOVERY=1447,
    TORMENT_DEATH=1448,
    TORMENT_UNDEAD=1450,
    TORMENTED_SCREAM=1451,
    TORTURE_DEATH=1452,
    TORTURE_UNDEAD=1454,
    TOUCH_OF_NIFE=1455,
    TOUGH_SKIN=1456,
    TOWERING_MIND=1457,
    TOWERING_WALL=1458,
    TOXIC_BLADE=1459,
    TOXIC_BOLT=1460,
    TOXIC_DOOM=1461,
    TOXINBLADE=1462,
    TOXOPHILITE=1463,
    TOXXULIA_CIRCLE=1464,
    TOXXULIAN_BLIGHT=1465,
    TRACKERS_INSTINCT=1466,
    TRANQUILITY=1467,
    TRANSFUSE=1468,
    TRANSFUSION=1469,
    TRANSMUTATIVE_MIXTURE=1470,
    TRAPEZISTS_POISE=1471,
    TRIBAL_BRAWN=1473,
    TRIBAL_FORCE=1474,
    TRIBAL_MIGHT=1475,
    TRIBAL_SPIRIT=1476,
    TRIBAL_STRENGTH=1477,
    TRIBAL_THEW=1478,
    TRIBAL_TOUGHNESS=1479,
    TRIBAL_WARD=1480,
    TRUE_FORM=1481,
    TRUE_SHOT=1484,
    TUMBLERS_GRACE=1485,
    TUNARES_BLESSING=1486,
    TURN_UNDEAD=1487,
    UMBRAL_COVENANT=1488,
    UMBRAL_CURSE=1489,
    UMBRAL_PACT=1490,
    UMBRAL_TOWER=1491,
    UNHOLY_BOND=1492,
    UNHOLY_GHOULISH_SACRAMENT=1493,
    UNITED_FAITH=1494,
    UNITY_ENLIGHTENMENT=1495,
    UNSTABLE_MIXTURE=1496,
    URSUS_GUISE=1497,
    VALOR=1498,
    VAMPIRIC_GA=1499,
    VANISH=1500,
    VAULTERS_BALANCE=1502,
    VEIL_OF_DEATH=1504,
    VELOCITY=1505,
    VENGEANCE=1506,
    VENOM_BOLT=1507,
    VENOM_CLOUD=1508,
    VENOMED_BLADE=1509,
    VENOMOUSBLADE=1510,
    VERDANT_HEALTH=1511,
    VESSEL_SPIRIT=1513,
    VEXATION=1514,
    VIGILANT_SPIRIT=1515,
    VILEBLADE=1516,
    VIRULENT_MIXTURE=1517,
    VITAL_ANTIDOTE=1518,
    VITAL_CURATIVE=1520,
    VITAL_PURITY=1521,
    VITALIZE=1523,
    VOLATILE_MIXTURE=1524,
    VOLATILE_SUBLIMATION=1525,
    VYLINNUZAS_MERCILESS=1526,
    VYLINNUZAS_MERCILESS_INFERNO=1527,
    VYLINNUZAS_TIDAL_DESTRUCTION=1528,
    WALKING_BONES=1529,
    WARCRY=1530,
    WARD_AFFLICTION=1531,
    WARD_DEATH=1532,
    WARD_ELEMENTS=1534,
    WARD_OF_BLESSINGS=1535,
    WASTING_MIST=1536,
    WATER_ELEMENTAL=1537,
    WATER_ELEMENTALING=1538,
    WATER_ELEMENTALKIN=1539,
    WAVE_OF_GLORY=1540,
    WAVE_OF_HEALTH=1541,
    WAVE_OF_LIFE=1542,
    WAVE_OF_RECOVERY=1543,
    WEAKEN_DEATH=1544,
    WHIRLING_BLADES=1545,
    WILD_GROWTH=1547,
    WILD_NATURE=1548,
    WILES=1549,
    WILTING_FLESH=1551,
    WILTING_WIND=1552,
    WINDBLADE=1553,
    WINGED_DEATH=1554,
    WINGED_DOOM=1556,
    WITHERING_WIND=1558,
    WOLF_FORM=1559,
    WOLFS_INSTINCT=1561,
    WONDERFUL_VACCINATION=1562,
    WOODSKIN=1563,
    WOUND=1564,
    WRAITHS_UNHOLY_SACRAME=1565,
    WRATH=1566,
    WRETCHEDBLADE=1567,
    YALGIMAS_FEARSOME_COMPANION=1568,
    YALGIMAS_MENACING_DEFENDER=1569,
    ZEALOUS_DEFENDER=1570,
    RETURN_HOME=1571,
    FOOD_AND_DRINK=1572,
    REMOVE_ILLUSION=1573,
    STONESKIN=1574,
    POWER_INDUCTION=1575,
    FINGERS_OF_THUNDER=1576,
    FINGERS_OF_FROST=1577,
    FINGERS_OF_FLAME=1578,
    WARLOCKS_WIGGLING_FINGERS=1579,
    SCORPION_FORM=1580,
    SCORPIONS_PESTILENCE=1581,
    VIBRANCE=1583,
    DIVINITY=1584,
    INFLUENCE=1585,
    RADIANCE=1586,
    LEOPARD_FORM=1587,
    LEOPARDS_ELEGANCE=1588,
    BRAVOS_DANCE=1589,
    POISE=1590,
    ARCANIC_ASSAULT=1591,
    DIVINE_ASSAULT=1592,
    WARMONGERS_ASSAULT=1593,
    BRAWLERS_AEGIS=1594,
    PANTHER_FORM=1595,
    PANTHERS_ALACRITY=1596,
    SOLDIERS_CRY=1597,
    FURY_OF_THE_AKESSAN=1598,
    PROTECTION_OF_MARR=1599,
    RAGE_OF_ZEK=1600,
    PLANAR_FURY=1601,
    MINOTAUR_FORM=1602,
    MINOTAURS_PERSISTANCE=1603,
    BERSERK=1604,
    INNORUUKS_AURA=1605,
    SPITE=1606,
    FLASH_OF_DAGGERS=1607,
    SHROUD_OF_HATE=1608,
    SACRAFICE=1609,
    BREW=1610,
    HAMMER_WIELDER=1611,
    PURE_HEART=1613,
    KEEN_EYE=1614,
    ERUDS_TEACHING=1615,
    CONSTRUCT=1616,
    REPAIR=1617,
    OIL_SLICK=1618,
    INVENTION=1619,
    CANTRIP=1620,
    PROTECT_THE_VALE=1621,
    HALFLING_HEROISM=1622,
    DISTRACT=1624,
    BAKE=1625,
    EXPOSE=1626,
    RETREAT=1627,
    LEADERSHIP=1628,
    CRUSH=1629,
    CLOBBER=1630,
    ENRAGE=1631,
    BATTLE_REGENERATION=1632,
    SWAMP_STENCH=1633,
    SWAMP_FRIEND_I=1634,
    SWAMP_FRIEND_II=1635,
    SWAMP_FRIEND_III=1636,
    GROWL_OF_BATTLE=1637,
    HOWL_OF_DOOM=1638,
    RAGE_OF_DRINAL=1639,
    HOWL_OF_TERROR=1640,
    DIRE_WOLF_FORM=1641,
    LIONS_NOBILITY=1642,
    AGILITY_OF_KEHRAHN=1643,
    ACCELERATED_FOLIAGE=1644,
    BEAR_NOBILITY=1645,
    CAVE_BEAR_STRENGTH=1646,
    SUMMON_BLESSED_FISH=1647,
    DEFENSE_OF_THE_SWARM=1648,
    CUNNING_SUBTERFUGE=1649,
    DISTRACTING_SQUEEKS=1650,
    SWARM_MENTALITY=1651,
    REPTILIAN_GAZE=1652,
    REPTILIAN_FRENZY=1653,
    SWAMP_VINES=1654,
    BITE=1655,
    LYCANTHROPIC_REGENERATION=1656,
    LYCANTHROPIC_REJUVENATION=1657,
    SPIRITMASTER=1658,
    WAYSENDER=1659
    }
    
    return spells