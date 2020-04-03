using TabletApp.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TabletApp
{
    public static class RealmManager
    {
        public static Realm Realm { get; private set; }

        private static List<(string ClassName, Func<RealmObject, RealmObject> Clone)> typesWithRemovalPrevention;

        static RealmManager()
        {
            var configuration = new RealmConfiguration() { SchemaVersion = 1 };
#if DEBUG
            configuration.ShouldDeleteIfMigrationNeeded = true;
#endif
            Realm = Realm.GetInstance(configuration);

            typesWithRemovalPrevention = new List<(string ClassName, Func<RealmObject, RealmObject> Clone)>();
        }

        public static void Write(Action action)
        {
            Realm.Write(action);
        }

        public static T Find<T>(string primaryKey) where T : RealmObject
        {
            return Realm.Find<T>(primaryKey);
        }

        public static IQueryable<T> All<T>() where T : RealmObject
        {
            return Realm.All<T>();
        }

        public static void AddOrUpdate<T>(T obj, bool inWriteTransaction = false) where T : RealmObject
        {
            if (inWriteTransaction)
            {
                Realm.Add(obj, true);
            }

            else
            {
                Realm.Write(() =>
                {
                    Realm.Add(obj, true);
                });
            }
        }

        public static void AddOrUpdate<T>(IEnumerable<T> list, bool inWriteTransaction = false) where T : RealmObject
        {
            if (list != null)
            {
                if (inWriteTransaction)
                {
                    foreach (var obj in list)
                    {
                        Realm.Add(obj, true);
                    }
                }
                else
                {
                    Realm.Write(() =>
                    {
                        foreach (var obj in list)
                        {
                            Realm.Add(obj, true);
                        }
                    });
                }
            }
        }

        public static void Remove<T>(IEnumerable<T> list, bool inWriteTransaction = false) where T : RealmObject
        {
            if (inWriteTransaction)
            {
                foreach (var obj in list)
                {
                    Realm.Remove(obj);
                }
            }
            else
            {
                Realm.Write(() =>
                {
                    foreach (var obj in list)
                    {
                        Realm.Remove(obj);
                    }
                });
            }
        }

        public static void RemoveAll<T>(bool inWriteTransaction = false) where T : RealmObject
        {
            if (inWriteTransaction)
            {
                Realm.RemoveAll<T>();
            }
            else
            {
                Realm.Write(() =>
                {
                    Realm.RemoveAll<T>();
                });
            }
        }
    }
}
