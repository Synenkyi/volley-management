﻿namespace VolleyManagement.Data.MsSql.Context.Migrations
{
    using System.Collections.Generic;
    using VolleyManagement.Data.MsSql.Entities;

    /// <summary>
    /// Class which contains pre-written teams.
    /// </summary>
    static class StoredContributors
    {
        private static List<ContributorTeamEntity> storedContributorTeamEntities =
            new List<ContributorTeamEntity>
        {
                ContributorsProMan(),
                Contributors042Net(),
                Contributors052Net(),
                Contributors061Net(),
                Contributors064Net(),
                Contributors064Atqc(),
                Contributors065Ui(),
                Contributors070Ui(),
                Contributors072Net(),
                Contributors076Atqc(),
                Contributors085Net(),
                Contributors091Atqc(),
                Contributors096Net(),
                Contributors103Net(),
                Contributors122Net()
        };

        internal static List<ContributorTeamEntity> GetAllStoredTeamEntities()
        {
            return storedContributorTeamEntities;
        }

        #region Contributor teams generators

        private static ContributorTeamEntity ContributorsProMan()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Project Management",
                CourseDirection = "All",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Sergii Diachenko" },
                    new ContributorEntity { Name = "Iurii Osipchuk" },
                    new ContributorEntity { Name = "Oleksii Konovalenko" },
                    new ContributorEntity { Name = "Dmytro Petin" },
                    new ContributorEntity { Name = "Oleg Shvets" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors042Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-042 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Igor Leta" },
                    new ContributorEntity { Name = "Dmytro Balayev" },
                    new ContributorEntity { Name = "Ielyzaveta Kalinchuk" },
                    new ContributorEntity { Name = "Oleg Petrushov" },
                    new ContributorEntity { Name = "Evgenij Kozhan" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors052Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-052 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Julia Bukova" },
                    new ContributorEntity { Name = "Alexandr Marchotko" },
                    new ContributorEntity { Name = "Pavlo Dragobetskij" },
                    new ContributorEntity { Name = "Anastacia Necheporenko" },
                    new ContributorEntity { Name = "Egor Meshcheriakov" },
                    new ContributorEntity { Name = "Pavel Goncharenko" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors061Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-061 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Nikita Gordienko" },
                    new ContributorEntity { Name = "Marina Gaponova" },
                    new ContributorEntity { Name = "Sofia Shaposhnik" },
                    new ContributorEntity { Name = "Pavlo Pokhylenko" },
                    new ContributorEntity { Name = "Sergii Kuzmin" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors064Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-064 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Andrii Zelyk" },
                    new ContributorEntity { Name = "Viktoriia Ryndina" },
                    new ContributorEntity { Name = "Alex Lapin" },
                    new ContributorEntity { Name = "Dmytro Chernyshov" },
                    new ContributorEntity { Name = "Maria Kochetkova" },
                    new ContributorEntity { Name = "Oleh Vovkodav" },
                    new ContributorEntity { Name = "Alexandr Maha" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors064Atqc()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-064 ATQC",
                CourseDirection = "ATQC",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Mykola Kolisnyk" },
                    new ContributorEntity { Name = "Maria Tsymbaliuk" },
                    new ContributorEntity { Name = "Ipatov Sergii" },
                    new ContributorEntity { Name = "Dmitriy Otrashevskiy" },
                    new ContributorEntity { Name = "Ruslan Borisenko" },
                    new ContributorEntity { Name = "Zavizion Stanislav" },
                    new ContributorEntity { Name = "Oleksandr Rudyi" },
                    new ContributorEntity { Name = "Sergey Bondarenko" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors065Ui()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-065 UI",
                CourseDirection = "UI",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Denis Chernysh" },
                    new ContributorEntity { Name = "Oleh Man'kov" },
                    new ContributorEntity { Name = "Kateryna Nikolaieva" },
                    new ContributorEntity { Name = "Stanislav Makhnyts'kyi" },
                    new ContributorEntity { Name = "Yevhen Alf'orov" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors070Ui()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-070 UI",
                CourseDirection = "UI",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Dykhov Egor" },
                    new ContributorEntity { Name = "Shafarenko Ol'ha" },
                    new ContributorEntity { Name = "Ovcharenko Ekateryna" },
                    new ContributorEntity { Name = "Korostienko Daniil" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors072Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-072 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Denis Rudenko" },
                    new ContributorEntity { Name = "Mykyta Kurchenkov" },
                    new ContributorEntity { Name = "Elyzaveta Rudakova" },
                    new ContributorEntity { Name = "Mihail Zazharskiy" },
                    new ContributorEntity { Name = "Dmitriy Ryndin" },
                    new ContributorEntity { Name = "Dmitriy Shapoval" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors076Atqc()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-076 ATQC",
                CourseDirection = "ATQC",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Alla Prykhodchenko" },
                    new ContributorEntity { Name = "Andriy Lantukh" },
                    new ContributorEntity { Name = "Oleksandr Zaitsev" },
                    new ContributorEntity { Name = "Artem Pychenko" },
                    new ContributorEntity { Name = "Dmytro Maslov" },
                    new ContributorEntity { Name = "Artem  Pozdeev" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors085Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-085 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Ievgen Oparyshev" },
                    new ContributorEntity { Name = "Mariia Shvets" },
                    new ContributorEntity { Name = "Eugene Gerbut" },
                    new ContributorEntity { Name = "Pavel Novichkhin" },
                    new ContributorEntity { Name = "Artem Kolisnyk" },
                    new ContributorEntity { Name = "Oleksii Khloptsev" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors091Atqc()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-091 ATQC",
                CourseDirection = "ATQC",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Daniil Zhyliaiev" },
                    new ContributorEntity { Name = "Sergiy Tsyganivskiy" },
                    new ContributorEntity { Name = "Yuriy Eugene" },
                    new ContributorEntity { Name = "Anton Karabaza" },
                    new ContributorEntity { Name = "Julia Bodnar" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors096Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-096 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Ievgen Oparyshev" },
                    new ContributorEntity { Name = "Anatolii Padenko" },
                    new ContributorEntity { Name = "Maxym Prisich" },
                    new ContributorEntity { Name = "Nataliya Tapchevskaya" },
                    new ContributorEntity { Name = "Daria Tatarchenko" },
                    new ContributorEntity { Name = "Mariia Hubenko" },
                    new ContributorEntity { Name = "Mykola Bocharskyi" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors103Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-103 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Artem Horbunov" },
                    new ContributorEntity { Name = "Kostiantyn Geiko" },
                    new ContributorEntity { Name = "Anastasiia Kolobovnykova" },
                    new ContributorEntity { Name = "Nataliia Karasevych" },
                    new ContributorEntity { Name = "Vladislav Antonyuk" },
                    new ContributorEntity { Name = "Dmitro Chepa" }
                }
            };
            return contributors;
        }

        private static ContributorTeamEntity Contributors122Net()
        {
            ContributorTeamEntity contributors = new ContributorTeamEntity
            {
                Name = "Dp-122 .NET",
                CourseDirection = ".NET",
                Contributors = new List<ContributorEntity>
                {
                    new ContributorEntity { Name = "Anton Bilyi" },
                    new ContributorEntity { Name = "Andrii Sherman" },
                    new ContributorEntity { Name = "Dmytro Lohunov" },
                    new ContributorEntity { Name = "Oleksandr Leontiev" },
                    new ContributorEntity { Name = "Sofia Babenko" }
                }
            };
            return contributors;
        }

        #endregion
    }
}
